using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GBAIntroManager
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        OpenFileDialog ofd = new OpenFileDialog();
        string gameCode = string.Empty; //Makes a new string for the gameCode.
        string gameName = string.Empty;
        bool posUnlocked = true;
        bool truckRemoved = true;
        Int32 pkmnPictureTable;
        Int32 pkmnPaletteTable;
        private void btnLoadROM_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.Filter = "GBA ROM (*.gba)|*.gba"; //Makes a filter for the open file dialog.
                if (ofd.ShowDialog() == DialogResult.OK) //Shows the dialog and checks if the user chose a file
                {
                    using (BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName)))//This is the position that the BinaryReader will read the hex data. The code from 0xAC to 0xAF is the game version. It also creates another integer, used for the data.
                    {
                        br.BaseStream.Seek(0xAC, SeekOrigin.Begin);
                        gameCode = Encoding.ASCII.GetString(br.ReadBytes(4)); //Sets the string of the string "gameCode" to the four bytes of the game code.
                    }
                    getGameName();
                    labelLoadedROM.Text = "Loaded ROM: " + ofd.SafeFileName + " | " + gameName;
                    string[] gameCodeArray = { "AXVE", "AXPE", "BPRE", "BPGE", "BPEE" };
                    if (gameCodeArray.Contains(gameCode))
                    {
                        using (BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName)))
                        {
                            if (gameCode == "AXVE" || gameCode == "AXPE")
                            {
                                br.BaseStream.Seek(0x52E23, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x47)
                                {
                                    posUnlocked = true;
                                    textBoxXPos.Visible = true;
                                    textBoxYPos.Visible = true;
                                    buttonXYPosUnlock.Visible = false;
                                    br.BaseStream.Seek(0x52E10, SeekOrigin.Begin);
                                    textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x52E12, SeekOrigin.Begin);
                                    textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x52E14, SeekOrigin.Begin);
                                    textBoxXPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x52E0C, SeekOrigin.Begin);
                                    textBoxYPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                }
                                else
                                {
                                    posUnlocked = false;
                                    textBoxXPos.Visible = false;
                                    textBoxYPos.Visible = false;
                                    buttonXYPosUnlock.Visible = true;
                                    br.BaseStream.Seek(0x52E0E, SeekOrigin.Begin);
                                    textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x52E10, SeekOrigin.Begin);
                                    textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                }

                                boxVersionSpecific.Text = "Truck Removal";
                                buttonResetCry.Visible = false;
                                textBoxCry.Visible = false;
                                labelCry.Visible = false;
                                buttonTruckRemove.Visible = true;
                                textBoxSecsOnTitle.Enabled = false;
                                textBoxSecsOnTitle.ResetText();
                                textBoxIntroPKMN.MaxLength = 3;

                                UInt32 truckRemovedAddr;

                                if (gameCode == "AXVE")
                                    truckRemovedAddr = 0x0805450D;
                                else
                                    truckRemovedAddr = 0x08054511;

                                br.BaseStream.Seek(0xC76FC, SeekOrigin.Begin);
                                if (br.ReadUInt32() == truckRemovedAddr)
                                {
                                    truckRemoved = true;
                                    buttonTruckRemove.Text = "Truck Removed";
                                    buttonTruckRemove.Enabled = false;
                                }
                                else
                                {
                                    truckRemoved = false;
                                    buttonTruckRemove.Text = "Remove the Truck";
                                    buttonTruckRemove.Enabled = true;
                                }

                                br.BaseStream.Seek(0xB286, SeekOrigin.Begin);
                                UInt16 tempSpace = br.ReadByte();
                                br.BaseStream.Seek(0xB288, SeekOrigin.Begin);
                                int testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x64)
                                    tempSpace *= 2;
                                textBoxIntroPKMN.Text = Convert.ToString(tempSpace, 0x10);

                                br.BaseStream.Seek(0xD324, SeekOrigin.Begin);
                                pkmnPictureTable = br.ReadInt32();
                                br.BaseStream.Seek(0x40954, SeekOrigin.Begin);
                                pkmnPaletteTable = br.ReadInt32();

                                if (gameCode == "AXVE")
                                    br.BaseStream.Seek(0x4062F0, SeekOrigin.Begin);
                                else
                                    br.BaseStream.Seek(0x406348, SeekOrigin.Begin);
                                textBoxPCItemID.Text = Convert.ToString(br.ReadUInt16(), 0x10);
                                textBoxPCItemAmt.Text = Convert.ToString(br.ReadUInt16());

                                br.BaseStream.Seek(0x52F4C, SeekOrigin.Begin);
                                textBoxMoney.Text = Convert.ToString(br.ReadUInt32());

                                br.BaseStream.Seek(0x7C40C, SeekOrigin.Begin);
                                textBoxTitleMusic.Text = Convert.ToString(br.ReadUInt32(), 0x10);

                                br.BaseStream.Seek(0xA29E, SeekOrigin.Begin);
                                tempSpace = br.ReadByte();
                                br.BaseStream.Seek(0xA2A0, SeekOrigin.Begin);
                                testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x40)
                                    tempSpace *= 2;
                                textBoxProfMusic.Text = Convert.ToString(tempSpace, 0x10);

                                br.BaseStream.Seek(0xA758, SeekOrigin.Begin);
                                if (br.ReadUInt32() == 0x0800A975)
                                    checkBoxSkipGender.Checked = true;
                                else
                                    checkBoxSkipGender.Checked = false;

                            }
                            else if (gameCode == "BPEE")
                            {
                                br.BaseStream.Seek(0x8446B, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x47)
                                {
                                    posUnlocked = true;
                                    textBoxXPos.Visible = true;
                                    textBoxYPos.Visible = true;
                                    buttonXYPosUnlock.Visible = false;
                                    br.BaseStream.Seek(0x84458, SeekOrigin.Begin);
                                    textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x8445A, SeekOrigin.Begin);
                                    textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x8445C, SeekOrigin.Begin);
                                    textBoxXPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x84454, SeekOrigin.Begin);
                                    textBoxYPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                }
                                else
                                {
                                    posUnlocked = false;
                                    textBoxXPos.Visible = false;
                                    textBoxYPos.Visible = false;
                                    buttonXYPosUnlock.Visible = true;
                                    br.BaseStream.Seek(0x84456, SeekOrigin.Begin);
                                    textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                    br.BaseStream.Seek(0x84458, SeekOrigin.Begin);
                                    textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                }

                                boxVersionSpecific.Text = "Truck Removal";
                                buttonResetCry.Visible = false;
                                textBoxCry.Visible = false;
                                labelCry.Visible = false;
                                buttonTruckRemove.Visible = true;
                                textBoxSecsOnTitle.Enabled = false;
                                textBoxSecsOnTitle.ResetText();
                                textBoxIntroPKMN.MaxLength = 3;

                                br.BaseStream.Seek(0xFB53C, SeekOrigin.Begin);
                                if (br.ReadUInt32() == 0x08085FFD)
                                {
                                    truckRemoved = true;
                                    buttonTruckRemove.Text = "Truck Removed";
                                    buttonTruckRemove.Enabled = false;
                                }
                                else
                                {
                                    truckRemoved = false;
                                    buttonTruckRemove.Text = "Remove the Truck";
                                    buttonTruckRemove.Enabled = true;
                                }

                                br.BaseStream.Seek(0x31924, SeekOrigin.Begin);
                                textBoxIntroPKMN.Text = Convert.ToString(br.ReadUInt32(), 0x10);

                                br.BaseStream.Seek(0x5DFEFC, SeekOrigin.Begin);
                                textBoxPCItemID.Text = Convert.ToString(br.ReadUInt16(), 0x10);
                                textBoxPCItemAmt.Text = Convert.ToString(br.ReadUInt16(), 0x10);

                                br.BaseStream.Seek(0x845BC, SeekOrigin.Begin);
                                textBoxMoney.Text = Convert.ToString(br.ReadUInt32());

                                br.BaseStream.Seek(0xAAAE8, SeekOrigin.Begin);
                                textBoxTitleMusic.Text = Convert.ToString(br.ReadUInt32(), 0x10);

                                br.BaseStream.Seek(0x30872, SeekOrigin.Begin);
                                UInt16 tempSpace = br.ReadByte();
                                br.BaseStream.Seek(0x30874, SeekOrigin.Begin);
                                UInt16 testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x40)
                                    tempSpace *= 2;
                                textBoxProfMusic.Text = Convert.ToString(tempSpace, 0x10);

                                br.BaseStream.Seek(0x30DC4, SeekOrigin.Begin);
                                if (br.ReadUInt32() == 0x08030FD5)
                                    checkBoxSkipGender.Checked = true;
                                else
                                    checkBoxSkipGender.Checked = false;

                            }
                            else if (gameCode == "BPRE" | gameCode == "BPGE")
                            {
                                posUnlocked = true;
                                textBoxXPos.Visible = true;
                                textBoxYPos.Visible = true;
                                buttonResetCry.Visible = true;
                                textBoxCry.Visible = true;
                                labelCry.Visible = true;
                                buttonXYPosUnlock.Visible = false;
                                boxVersionSpecific.Text = "Titlescreen Pokémon Cry";
                                buttonTruckRemove.Visible = false;
                                textBoxSecsOnTitle.Enabled = true;
                                textBoxIntroPKMN.MaxLength = 2;
                                br.BaseStream.Seek(0x54A04, SeekOrigin.Begin);
                                textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(0x54A06, SeekOrigin.Begin);
                                textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(0x54A08, SeekOrigin.Begin);
                                textBoxXPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(0x54A00, SeekOrigin.Begin);
                                textBoxYPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(0x791EE, SeekOrigin.Begin);
                                UInt16 cryNumber = br.ReadByte();
                                br.BaseStream.Seek(0x791F0, SeekOrigin.Begin);
                                textBoxCry.Text = Convert.ToString(cryNumber + br.ReadByte(), 0x10);
                                if (gameCode == "BPRE")
                                    br.BaseStream.Seek(0x130F4C, SeekOrigin.Begin);
                                else
                                    br.BaseStream.Seek(0x130F24, SeekOrigin.Begin);
                                textBoxIntroPKMN.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(0x128, SeekOrigin.Begin);
                                pkmnPictureTable = br.ReadInt32();
                                br.BaseStream.Seek(0x130, SeekOrigin.Begin);
                                pkmnPaletteTable = br.ReadInt32();

                                if (gameCode == "BPRE")
                                    br.BaseStream.Seek(0x402220, SeekOrigin.Begin);
                                else
                                    br.BaseStream.Seek(0x40205C, SeekOrigin.Begin);
                                textBoxPCItemID.Text = Convert.ToString(br.ReadUInt16(), 0x10);
                                textBoxPCItemAmt.Text = Convert.ToString(br.ReadUInt16(), 0x10);

                                br.BaseStream.Seek(0x54B60, SeekOrigin.Begin);
                                textBoxMoney.Text = Convert.ToString(br.ReadUInt32());

                                br.BaseStream.Seek(0x78AF4, SeekOrigin.Begin);
                                UInt16 tempSpace = br.ReadByte();
                                br.BaseStream.Seek(0x78AF6, SeekOrigin.Begin);
                                UInt16 testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x40)
                                    tempSpace *= 2;
                                textBoxTitleMusic.Text = Convert.ToString(tempSpace, 0x10);

                                if (gameCode == "BPRE")
                                {
                                    br.BaseStream.Seek(0x12F836, SeekOrigin.Begin);
                                    tempSpace = br.ReadByte();
                                    br.BaseStream.Seek(0x12F838, SeekOrigin.Begin);
                                    testCase = br.ReadByte();
                                }
                                else
                                {
                                    br.BaseStream.Seek(0x12F80E, SeekOrigin.Begin);
                                    tempSpace = br.ReadByte();
                                    br.BaseStream.Seek(0x12F810, SeekOrigin.Begin);
                                    testCase = br.ReadByte();
                                }

                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x40)
                                    tempSpace *= 2;
                                textBoxProfMusic.Text = Convert.ToString(tempSpace, 0x10);

                                br.BaseStream.Seek(0x78C1C, SeekOrigin.Begin);
                                textBoxSecsOnTitle.Text = Convert.ToString((br.ReadUInt32() + 1) / 60);

                                br.BaseStream.Seek(0x12FDA4, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x00)
                                    checkBoxSkipGender.Checked = true;
                                else
                                    checkBoxSkipGender.Checked = false;
                            }
                            enableEverything();
                        }
                    }
                    else
                    {
                        disableEverything();
                        MessageBox.Show("Unsupported ROM loaded. Please load an English language 3rd generation Pokémon game.","Unsupported ROM",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Could not open ROM file.\nCheck to see if the file is open in another program.", "File in Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length / 2;
            byte[] bytes = new byte[NumberChars];
            using (var sr = new StringReader(hex))
            {
                for (int i = 0; i < NumberChars; i++)
                    bytes[i] =
                      Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return bytes;
        }

        public void getGameName()
        {
            if (gameCode == "AXVE")
                gameName = "Pokémon Ruby";
            else if (gameCode == "AXPE")
                gameName = "Pokémon Sapphire";
            else if (gameCode == "BPRE")
                gameName = "Pokémon FireRed";
            else if (gameCode == "BPGE")
                gameName = "Pokémon LeafGreen";
            else if (gameCode == "BPEE")
                gameName = "Pokémon Emerald";
            else
                gameName = "Unknown Base ROM";
        }
        public void enableEverything()
        {
            btnSave.Enabled = true;
            buttonStartPosReset.Enabled = true;
            buttonResetCry.Enabled = true;
            buttonResetIntroPKMN.Enabled = true;
            buttonResetMusic.Enabled = true;
            buttonResetStartItems.Enabled = true;
            textBoxMapBank.Enabled = true;
            textBoxMap.Enabled = true;
            textBoxXPos.Enabled = true;
            textBoxYPos.Enabled = true;
            textBoxCry.Enabled = true;
            textBoxIntroPKMN.Enabled = true;
            textBoxTitleMusic.Enabled = true;
            textBoxProfMusic.Enabled = true;
            textBoxPCItemID.Enabled = true;
            textBoxPCItemAmt.Enabled = true;
            textBoxMoney.Enabled = true;
            checkBoxSkipGender.Enabled = true;
        }

        public void disableEverything()
        {
            btnSave.Enabled = false;
            buttonStartPosReset.Enabled = false;
            buttonResetCry.Enabled = false;
            buttonResetIntroPKMN.Enabled = false;
            buttonResetMusic.Enabled = false;
            buttonResetStartItems.Enabled = false;
            textBoxMapBank.Enabled = false;
            textBoxMapBank.ResetText();
            textBoxMap.Enabled = false;
            textBoxMap.ResetText();
            textBoxXPos.Enabled = false;
            textBoxXPos.ResetText();
            textBoxYPos.Enabled = false;
            textBoxYPos.ResetText();
            textBoxCry.Enabled = false;
            textBoxCry.ResetText();
            textBoxIntroPKMN.Enabled = false;
            textBoxIntroPKMN.ResetText();
            textBoxTitleMusic.Enabled = false;
            textBoxTitleMusic.ResetText();
            textBoxProfMusic.Enabled = false;
            textBoxProfMusic.ResetText();
            textBoxPCItemID.Enabled = false;
            textBoxPCItemID.ResetText();
            textBoxPCItemAmt.Enabled = false;
            textBoxPCItemAmt.ResetText();
            textBoxMoney.Enabled = false;
            textBoxMoney.ResetText();
            textBoxSecsOnTitle.Enabled = false;
            textBoxSecsOnTitle.ResetText();
            checkBoxSkipGender.Enabled = false;
            checkBoxSkipGender.Checked = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxMapBank_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxMapBank.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxMapBank.Text = item.Remove(item.Length - 1, 1);
                textBoxMapBank.SelectionStart = textBoxMapBank.Text.Length;
            }
        }

        private void textBoxMap_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxMap.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxMap.Text = item.Remove(item.Length - 1, 1);
                textBoxMap.SelectionStart = textBoxMap.Text.Length;
            }
        }

        private void textBoxXPos_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxXPos.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxXPos.Text = item.Remove(item.Length - 1, 1);
                textBoxXPos.SelectionStart = textBoxXPos.Text.Length;
            }
        }

        private void textBoxYPos_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxYPos.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxYPos.Text = item.Remove(item.Length - 1, 1);
                textBoxYPos.SelectionStart = textBoxYPos.Text.Length;
            }
        }

        private void buttonStartPosReset_Click(object sender, EventArgs e)
        {
            if (gameCode == "AXVE" | gameCode == "AXPE" | gameCode == "BPEE")
            {
                textBoxMapBank.Text = "19";
                textBoxMap.Text = "28";
                textBoxXPos.Text = "2";
                textBoxYPos.Text = "2";
            }
            else if (gameCode == "BPRE" | gameCode == "BPGE")
            {
                textBoxMapBank.Text = "4";
                textBoxMap.Text = "1";
                textBoxXPos.Text = "6";
                textBoxYPos.Text = "6";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName)))
                {
                    if (gameCode == "AXVE" || gameCode == "AXPE")
                    {
                        if (posUnlocked == true)
                        {
                            bw.Seek(0x52E10, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                            bw.Seek(0x52E12, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                            bw.Seek(0x52E14, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxXPos.Text, 0x10));
                            bw.Seek(0x52E0C, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxYPos.Text, 0x10));
                        }
                        else
                        {
                            bw.Seek(0x52E0E, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                            bw.Seek(0x52E10, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                        }

                        Int32 pkmnPictureOffset = pkmnPictureTable + (Convert.ToUInt16(textBoxIntroPKMN.Text, 0x10) * 8);
                        Int32 pkmnPaletteOffset = pkmnPaletteTable + (Convert.ToUInt16(textBoxIntroPKMN.Text, 0x10) * 8);

                        bw.Seek(0xB2B8, SeekOrigin.Begin);
                        bw.Write((UInt32)pkmnPictureOffset);
                        bw.Seek(0xB2C4, SeekOrigin.Begin);
                        bw.Write((UInt32)pkmnPaletteOffset);

                        UInt16 introPKMN = Convert.ToUInt16(textBoxIntroPKMN.Text, 0x10);
                        if (introPKMN > 0xFF)
                        {
                            introPKMN -= 0xFF;
                            bw.Seek(0xB288, SeekOrigin.Begin);
                            bw.Write((UInt16)0x34FF);
                            bw.Seek(0xA508, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(0xB288, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3400);
                            bw.Seek(0xA508, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }
                        bw.Seek(0xB286, SeekOrigin.Begin);
                        bw.Write((byte)introPKMN);
                        bw.Seek(0xA506, SeekOrigin.Begin);
                        bw.Write((byte)introPKMN);

                        if (gameCode == "AXVE")
                            bw.Seek(0x4062F0, SeekOrigin.Begin);
                        else
                            bw.Seek(0x406348, SeekOrigin.Begin);
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemID.Text, 0x10));
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemAmt.Text));

                        bw.Seek(0x52F4C, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxMoney.Text));

                        bw.Seek(0x7C40C, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxTitleMusic.Text, 0x10));

                        UInt16 profMusic = Convert.ToUInt16(textBoxProfMusic.Text, 0x10);
                        if (profMusic > 0xFF)
                        {
                            profMusic -= 0xFF;
                            bw.Seek(0xA2A0, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(0xA2A0, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }
                        bw.Seek(0xA29E, SeekOrigin.Begin);
                        bw.Write((byte)profMusic);

                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(0xA758, SeekOrigin.Begin);
                            bw.Write((UInt32)0x0800A975);
                            bw.Seek(0xAA04, SeekOrigin.Begin);
                            bw.Write((UInt16)0xE04A);
                            bw.Seek(0xAC1C, SeekOrigin.Begin);
                            bw.Write((UInt32)0x0800A975);
                        }
                        else
                        {
                            bw.Seek(0xA758, SeekOrigin.Begin);
                            bw.Write((UInt32)0x0800A75D);
                            bw.Seek(0xAA04, SeekOrigin.Begin);
                            bw.Write((UInt16)0x2001);
                            bw.Seek(0xAC1C, SeekOrigin.Begin);
                            bw.Write((UInt32)0x0800A75D);
                        }

                    }
                    else if (gameCode == "BPEE")
                    {
                        if (posUnlocked == true)
                        {
                            bw.Seek(0x84458, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                            bw.Seek(0x8445A, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                            bw.Seek(0x8445C, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxXPos.Text, 0x10));
                            bw.Seek(0x84454, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxYPos.Text, 0x10));
                        }
                        else
                        {
                            bw.Seek(0x84456, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                            bw.Seek(0x84458, SeekOrigin.Begin);
                            bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                        }
                        bw.Seek(0x31924, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxIntroPKMN.Text, 0x10));
                        bw.Seek(0x30B0C, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxIntroPKMN.Text, 0x10));

                        bw.Seek(0x5DFEFC, SeekOrigin.Begin);
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemID.Text, 0x10));
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemAmt.Text));

                        bw.Seek(0x845BC, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxMoney.Text));

                        bw.Seek(0xAAAE8, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxTitleMusic.Text, 0x10));

                        UInt16 profMusic = Convert.ToUInt16(textBoxProfMusic.Text, 0x10);
                        if (profMusic > 0xFF)
                        {
                            profMusic -= 0xFF;
                            bw.Seek(0x30874, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(0x30874, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }
                        bw.Seek(0x30872, SeekOrigin.Begin);
                        bw.Write((byte)profMusic);

                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(0x30DC4, SeekOrigin.Begin);
                            bw.Write((UInt32)0x08030FD5);
                            bw.Seek(0x3121C, SeekOrigin.Begin);
                            bw.Write((UInt32)0x08030FD5);
                        }
                        else
                        {
                            bw.Seek(0x30DC4, SeekOrigin.Begin);
                            bw.Write((UInt32)0x08030DC9);
                            bw.Seek(0x3121C, SeekOrigin.Begin);
                            bw.Write((UInt32)0x08030DC9);
                        }
                    }
                    else if (gameCode == "BPRE" | gameCode == "BPGE")
                    {
                        bw.Seek(0x54A04, SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                        bw.Seek(0x54A06, SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                        bw.Seek(0x54A08, SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxXPos.Text, 0x10));
                        bw.Seek(0x54A00, SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxYPos.Text, 0x10));
                        UInt16 cryNumber = Convert.ToUInt16(textBoxCry.Text, 0x10);
                        if (cryNumber > 0xFF)
                        {
                            cryNumber -= 0xFF;
                            byte[] cryPatch = { 0xC0, 0x79, 0x80, 0x21, 0x01, 0x40, 0x09, 0x06, 0x0D, 0x0E };
                            bw.Seek(0x791E0, SeekOrigin.Begin);
                            bw.Write((byte[])cryPatch);
                            bw.Seek(0x791F0, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(0x791F0, SeekOrigin.Begin);
                            bw.Write((byte)0x00);
                        }
                        bw.Seek(0x791EE, SeekOrigin.Begin);
                        bw.Write((byte)cryNumber);

                        Int32 pkmnPictureOffset = pkmnPictureTable + (Convert.ToByte(textBoxIntroPKMN.Text, 0x10) * 8);
                        Int32 pkmnPaletteOffset = pkmnPaletteTable + (Convert.ToByte(textBoxIntroPKMN.Text, 0x10) * 8);

                        if (gameCode == "BPRE")
                        {
                            bw.Seek(0x130FA0, SeekOrigin.Begin);
                            bw.Write((UInt32)pkmnPictureOffset);
                            bw.Seek(0x130FA4, SeekOrigin.Begin);
                            bw.Write((UInt32)pkmnPaletteOffset);
                            bw.Seek(0x130F4C, SeekOrigin.Begin);
                        }
                        else
                        {
                            bw.Seek(0x130F78, SeekOrigin.Begin);
                            bw.Write((UInt32)pkmnPictureOffset);
                            bw.Seek(0x130F7C, SeekOrigin.Begin);
                            bw.Write((UInt32)pkmnPaletteOffset);
                            bw.Seek(0x130F24, SeekOrigin.Begin);
                        }

                        byte[] introPKMNFix = { Convert.ToByte(textBoxIntroPKMN.Text, 0x10), 0x20, 0x00, 0x21};
                        bw.Write((byte[])introPKMNFix);

                        bw.Seek(0x12FB38, SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxIntroPKMN.Text, 0x10));

                        if (gameCode == "BPRE")
                            bw.Seek(0x402220, SeekOrigin.Begin);
                        else
                            bw.Seek(0x40205C, SeekOrigin.Begin);
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemID.Text, 0x10));
                        bw.Write((UInt16)Convert.ToUInt16(textBoxPCItemAmt.Text));

                        bw.Seek(0x54B60, SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxMoney.Text));

                        UInt16 titleMusic = Convert.ToUInt16(textBoxTitleMusic.Text, 0x10);
                        if (titleMusic > 0xFF)
                        {
                            titleMusic -= 0xFF;
                            bw.Seek(0x78AF6, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(0x78AF6, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }
                        bw.Seek(0x78AF4, SeekOrigin.Begin);
                        bw.Write((byte)titleMusic);

                        if (gameCode == "BPRE")
                            bw.Seek(0x12F838, SeekOrigin.Begin);
                        else
                            bw.Seek(0x12F810, SeekOrigin.Begin);
                        UInt16 profMusic = Convert.ToUInt16(textBoxProfMusic.Text, 0x10);
                        if (profMusic > 0xFF)
                        {
                            profMusic -= 0xFF;
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Write((UInt16)0x3000);
                        }

                        if (gameCode == "BPRE")
                            bw.Seek(0x12F836, SeekOrigin.Begin);
                        else
                            bw.Seek(0x12F80E, SeekOrigin.Begin);
                        bw.Write((byte)profMusic);

                        bw.Seek(0x78C1C, SeekOrigin.Begin);
                        bw.Write((UInt32)(Convert.ToUInt32(textBoxSecsOnTitle.Text) * 60) - 1);

                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(0x12FDA4, SeekOrigin.Begin);
                            bw.Write((byte)0x00);
                            bw.Seek(0x12FDFC, SeekOrigin.Begin);
                            bw.Write((UInt16)0xE00A);
                            bw.Seek(0x12FEBE, SeekOrigin.Begin);
                            bw.Write((UInt16)0xE05C);
                            bw.Seek(0x12FFAA, SeekOrigin.Begin);
                            bw.Write((UInt16)0x2100);
                            bw.Write((UInt16)0xE00C);
                        }
                        else
                        {
                            bw.Seek(0x12FDA4, SeekOrigin.Begin);
                            bw.Write((byte)0x30);
                            bw.Seek(0x12FDFC, SeekOrigin.Begin);
                            bw.Write((UInt16)0x2000);
                            bw.Seek(0x12FEBE, SeekOrigin.Begin);
                            bw.Write((UInt16)0x8C6C);
                            bw.Seek(0x12FFAA, SeekOrigin.Begin);
                            bw.Write((UInt16)0xF7DF);
                            bw.Write((UInt16)0xFD2B);
                        }

                    }

                }
                MessageBox.Show("File saved successfully!","Saved",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            catch (IOException)
            {
                MessageBox.Show("Could not save ROM file.\nCheck to see if the file is open in another program.", "File in Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GBA Intro Manager v0.0.1\nCreated by diegoisawesome.\nhttp://domoreaweso.me\n\nThanks to:\ncolcolstyles\nJambo51\nxGal","About");
        }

        private void btnReadme_Click(object sender, EventArgs e)
        {
            try
            {
                string fileLoc = System.Windows.Forms.Application.StartupPath + @"\Readme.txt";
                Process.Start(fileLoc);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Could not open Readme.txt. Did you delete it?", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonXYPosUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                byte originalMapBank;
                byte originalMap;
                if (gameCode == "AXVE" || gameCode == "AXPE")
                {
                    using (BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName)))
                    {
                        br.BaseStream.Seek(0x52E0E, SeekOrigin.Begin);
                        originalMapBank = br.ReadByte();
                        br.BaseStream.Seek(0x52E10, SeekOrigin.Begin);
                        originalMap = br.ReadByte();
                    }

                    using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName)))
                    {
                        byte[] introPatch = { 0x01, 0x22, 0x52, 0x42, 0x02, 0x20, 0x00, 0x90, originalMapBank, 0x20, originalMap, 0x21, 0x02, 0x23, 0x00, 0xF0, 0x1D, 0xFB, 0x00, 0xF0, 0x11, 0xFB, 0x01, 0xB0, 0x01, 0xBC, 0x00, 0x47 };
                        bw.Seek(0x52E08, SeekOrigin.Begin);
                        bw.Write((byte[])introPatch);
                    }
                }
                else if (gameCode == "BPEE")
                {

                    using (BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName)))
                    {
                        br.BaseStream.Seek(0x84456, SeekOrigin.Begin);
                        originalMapBank = br.ReadByte();
                        br.BaseStream.Seek(0x84458, SeekOrigin.Begin);
                        originalMap = br.ReadByte();
                    }

                    using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName)))
                    {
                        byte[] introPatch = { 0x01, 0x22, 0x52, 0x42, 0x02, 0x20, 0x00, 0x90, originalMapBank, 0x20, originalMap, 0x21, 0x02, 0x23, 0x00, 0xF0, 0xC5, 0xFB, 0x00, 0xF0, 0xB9, 0xFB, 0x01, 0xB0, 0x01, 0xBC, 0x00, 0x47 };
                        bw.Seek(0x52E08, SeekOrigin.Begin);
                        bw.Write((byte[])introPatch);
                    }

                }
                posUnlocked = true;
                textBoxXPos.Text = "2";
                textBoxXPos.Visible = true;
                textBoxYPos.Text = "2";
                textBoxYPos.Visible = true;
                buttonXYPosUnlock.Visible = false;
            }
            catch (IOException)
            {
                MessageBox.Show("Could not write to ROM file.\nCheck to see if the file is open in another program.", "File in Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxCry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonTruckRemove_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName)))
                {
                    if (gameCode == "AXVE")
                    {
                        bw.Seek(0xC76FC, SeekOrigin.Begin);
                        bw.Write((UInt32)0x0805450D);
                    }
                    else if (gameCode == "AXPE")
                    {
                        bw.Seek(0xC76FC, SeekOrigin.Begin);
                        bw.Write((UInt32)0x08054511);
                    }
                    else
                    {
                        bw.Seek(0xFB53C, SeekOrigin.Begin);
                        bw.Write((UInt32)0x08085FFD);
                    }
                }
                truckRemoved = true;
                buttonTruckRemove.Text = "Truck Removed";
                buttonTruckRemove.Enabled = false;
            }
            catch (IOException)
            {
                MessageBox.Show("Could not write to ROM file.\nCheck to see if the file is open in another program.", "File in Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxCry_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxCry.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxCry.Text = item.Remove(item.Length - 1, 1);
                textBoxCry.SelectionStart = textBoxCry.Text.Length;
            }
            else if (n > 0x1FE)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxCry.Text = "1FE";
            }

        }

        private void buttonResetCry_Click(object sender, EventArgs e)
        {
            if (gameCode == "BPRE")
                textBoxCry.Text = "6";
            if (gameCode == "BPGE")
                textBoxCry.Text = "3";
        }

        private void textBoxIntroPKMN_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxIntroPKMN.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxIntroPKMN.Text = item.Remove(item.Length - 1, 1);
                textBoxIntroPKMN.SelectionStart = textBoxIntroPKMN.Text.Length;
            }
            else if ((n > 0x1FE) && (gameCode == "AXVE" || gameCode == "AXPE"))
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxIntroPKMN.Text = "1FE";
            }
        }

        private void buttonResetIntroPKMN_Click(object sender, EventArgs e)
        {
            if (gameCode == "AXVE" | gameCode == "AXPE")
                textBoxIntroPKMN.Text = "15E";
            else if (gameCode == "BPRE" | gameCode == "BPGE")
                textBoxIntroPKMN.Text = "1D";
            else
                textBoxIntroPKMN.Text = "127";
        }

        private void buttonResetStartItems_Click(object sender, EventArgs e)
        {
            textBoxPCItemID.Text = "D";
            textBoxPCItemAmt.Text = "1";
            textBoxMoney.Text = "3000";
        }

        private void buttonResetMusic_Click(object sender, EventArgs e)
        {
            if (gameCode == "AXVE" | gameCode == "AXPE")
            {
                textBoxTitleMusic.Text = "19D";
                textBoxProfMusic.Text = "176";
            }
            else if (gameCode == "BPRE" | gameCode == "BPGE")
            {
                textBoxTitleMusic.Text = "116";
                textBoxProfMusic.Text = "124";
            }
            else
            {
                textBoxTitleMusic.Text = "19D";
                textBoxProfMusic.Text = "176";
            }
        }

        private void textBoxTitleMusic_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxTitleMusic.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxTitleMusic.Text = item.Remove(item.Length - 1, 1);
                textBoxTitleMusic.SelectionStart = textBoxTitleMusic.Text.Length;
            }
            else if ((n > 0x1FE) && (gameCode == "BPRE" || gameCode == "BPGE"))
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxTitleMusic.Text = "1FE";
            }
        }

        private void textBoxProfMusic_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxProfMusic.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxProfMusic.Text = item.Remove(item.Length - 1, 1);
                textBoxProfMusic.SelectionStart = textBoxProfMusic.Text.Length;
            }
            else if (n > 0x1FE)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxProfMusic.Text = "1FE";
            }
        }

        private void textBoxPCItemID_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxPCItemID.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxPCItemID.Text = item.Remove(item.Length - 1, 1);
                textBoxPCItemID.SelectionStart = textBoxPCItemID.Text.Length;
            }
        }

        private void textBoxPCItemAmt_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxPCItemAmt.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.Number, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxPCItemAmt.Text = item.Remove(item.Length - 1, 1);
                textBoxPCItemAmt.SelectionStart = textBoxPCItemAmt.Text.Length;
            }
        }

        private void textBoxMoney_TextChanged(object sender, EventArgs e)
        {
            string item = textBoxMoney.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.Number, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              item != String.Empty)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxMoney.Text = item.Remove(item.Length - 1, 1);
                textBoxMoney.SelectionStart = textBoxMoney.Text.Length;
            }
            else if (n > 999999)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxMoney.Text = "999999";
            }
        }

    }
}
