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
            characterValues = ReadTableFile(AppDomain.CurrentDomain.BaseDirectory + "/Table.ini");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        OpenFileDialog ofd = new OpenFileDialog();
        string currentROM;
        string gameCode;
        string gameName;
        string gameType;
        bool posUnlocked = true;
        private uint unlockedTester;
        private uint truckRemovedTester;
        private uint truckRemovedAddr;
        private uint titlescreenMusic;
        private uint skipGender1;
        private uint skipGenderTester;
        private uint skipGenderOriginal;
        private uint skipGender2;
        private uint skipGender3;
        private uint skipGender4;
        private uint flashbackRemove;
        private uint rivalNamingRemove;
        private uint titlescreenCry;
        private uint titlescreenTime;
        private uint introPokemonNumber1;
        private uint introPokemonNumber2;
        private uint introPokemonNumber3;
        private uint introPokemonPicture;
        private uint introPokemonPalette;
        private uint startingPosition;
        private uint startingPCItem;
        private uint startingMoney;
        private uint professorMusic;
        private uint pkmnPictureTable;
        private uint pkmnPaletteTable;
        private uint itemTable;
        private ushort numberOfItems;
        private uint pokemonNamesLocation;
        private ushort numberOfPokemon;
        private Dictionary<byte, char> characterValues;

        private void btnLoadROM_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.Filter = "GBA ROM (*.gba)|*.gba"; //Makes a filter for the open file dialog.
                if (ofd.ShowDialog() == DialogResult.OK) //Shows the dialog and checks if the user chose a file
                {
                    currentROM = ofd.FileName;
                    using (BinaryReader br = new BinaryReader(File.OpenRead(currentROM)))//This is the position that the BinaryReader will read the hex data. The code from 0xAC to 0xAF is the game version. It also creates another integer, used for the data.
                    {
                        br.BaseStream.Seek(0xAC, SeekOrigin.Begin);
                        gameCode = Encoding.ASCII.GetString(br.ReadBytes(4)); //Sets the string of the string "gameCode" to the four bytes of the game code.
                    }
                    ParseINI(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/GBAIntroManager.ini"), gameCode);
                    labelLoadedROM.Text = "Loaded ROM: " + ofd.SafeFileName + " | " + gameName;
                    string[] gameCodeArray = { "AXVE", "AXPE", "BPRE", "BPGE", "BPEE" };
                    if (gameCodeArray.Contains(gameCode))
                    {
                        comboBoxCry.Items.Clear();
                        for (uint i = 0; i <= numberOfPokemon; i++)
                        {
                            if (i <= 0x1FE)
                                comboBoxCry.Items.Add(ROMCharactersToString(10, (uint)(0xB * i + pokemonNamesLocation)));
                        }

                        using (BinaryReader br = new BinaryReader(File.OpenRead(currentROM)))
                        {
                            posUnlocked = true;

                            UInt16 tempSpace;
                            UInt16 testCase;

                            if (gameType == "RS" | gameType == "E")
                            {
                                br.BaseStream.Seek(unlockedTester, SeekOrigin.Begin);
                                if (br.ReadByte() != 0x47)
                                    posUnlocked = false;

                                boxVersionSpecific.Text = "Truck Removal";
                                buttonResetCry.Visible = false;
                                comboBoxCry.Visible = false;
                                buttonTruckRemove.Visible = true;
                                textBoxSecsOnTitle.Enabled = false;
                                textBoxSecsOnTitle.ResetText();
                                buttonResetSecsOnTitle.Enabled = false;
                                checkBoxFlashback.Enabled = false;
                                checkBoxSkipRivalNaming.Enabled = false;

                                br.BaseStream.Seek(truckRemovedTester, SeekOrigin.Begin);
                                if (br.ReadUInt32() == truckRemovedAddr)
                                {
                                    buttonTruckRemove.Text = "Truck Removed";
                                    buttonTruckRemove.Enabled = false;
                                }
                                else
                                {
                                    buttonTruckRemove.Text = "Remove the Truck";
                                    buttonTruckRemove.Enabled = true;
                                }

                                br.BaseStream.Seek(titlescreenMusic, SeekOrigin.Begin);
                                textBoxTitleMusic.Text = Convert.ToString(br.ReadUInt32(), 0x10);

                                br.BaseStream.Seek(skipGender1, SeekOrigin.Begin);
                                if (br.ReadUInt32() == skipGenderTester)
                                    checkBoxSkipGender.Checked = true;
                                else
                                    checkBoxSkipGender.Checked = false;
                            }
                            else if (gameType == "FR" | gameType == "LG")
                            {
                                posUnlocked = true;
                                textBoxXPos.Visible = true;
                                textBoxYPos.Visible = true;
                                buttonResetCry.Visible = true;
                                comboBoxCry.Visible = true;
                                buttonXYPosUnlock.Visible = false;
                                boxVersionSpecific.Text = "Titlescreen Pokémon Cry";
                                buttonTruckRemove.Visible = false;
                                textBoxSecsOnTitle.Enabled = true;
                                buttonResetSecsOnTitle.Enabled = true;
                                checkBoxFlashback.Enabled = true;
                                checkBoxSkipRivalNaming.Enabled = true;

                                br.BaseStream.Seek(titlescreenCry, SeekOrigin.Begin);
                                UInt16 cryNumber = br.ReadByte();
                                br.BaseStream.Seek(titlescreenCry + 0x2, SeekOrigin.Begin);
                                int cryTester = cryNumber + br.ReadByte();
                                if (cryTester <= numberOfPokemon)
                                    comboBoxCry.SelectedIndex = cryTester;
                                else
                                    comboBoxCry.SelectedIndex = numberOfPokemon;

                                comboBoxIntroPKMN.Items.Clear();
                                for (uint i = 0; i <= numberOfPokemon; i++)
                                {
                                    if (i <= 0xFF)
                                        comboBoxIntroPKMN.Items.Add(ROMCharactersToString(10, (uint)(0xB * i + pokemonNamesLocation)));
                                }

                                br.BaseStream.Seek(introPokemonNumber1, SeekOrigin.Begin);
                                comboBoxIntroPKMN.SelectedIndex = br.ReadByte();

                                br.BaseStream.Seek(titlescreenMusic, SeekOrigin.Begin);
                                tempSpace = br.ReadByte();
                                br.BaseStream.Seek(titlescreenMusic + 0x2, SeekOrigin.Begin);
                                testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x40)
                                    tempSpace *= 2;
                                if (tempSpace <= 0x1FE)
                                    textBoxTitleMusic.Text = Convert.ToString(tempSpace, 0x10);
                                else
                                    textBoxTitleMusic.Text = "1FE";

                                br.BaseStream.Seek(titlescreenTime, SeekOrigin.Begin);
                                textBoxSecsOnTitle.Text = Convert.ToString((br.ReadUInt32() + 1) / 60);

                                br.BaseStream.Seek(rivalNamingRemove, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x00)
                                    checkBoxSkipRivalNaming.Checked = true;
                                else
                                    checkBoxSkipRivalNaming.Checked = false;

                                br.BaseStream.Seek(skipGender1, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x00)
                                    checkBoxSkipGender.Checked = true;
                                else
                                    checkBoxSkipGender.Checked = false;

                                br.BaseStream.Seek(flashbackRemove + 1, SeekOrigin.Begin);
                                if (br.ReadByte() == 0x1C)
                                    checkBoxFlashback.Checked = true;
                                else
                                    checkBoxFlashback.Checked = false;
                            }

                            if (gameType == "RS")
                            {
                                comboBoxIntroPKMN.Items.Clear();
                                for (uint i = 0; i <= numberOfPokemon; i++)
                                {
                                    if (i <= 0x1FE)
                                        comboBoxIntroPKMN.Items.Add(ROMCharactersToString(10, (uint)(0xB * i + pokemonNamesLocation)));
                                }

                                br.BaseStream.Seek(introPokemonNumber1, SeekOrigin.Begin);
                                tempSpace = br.ReadByte();
                                br.BaseStream.Seek(introPokemonNumber1 + 2, SeekOrigin.Begin);
                                testCase = br.ReadByte();
                                if (testCase == 0xFF)
                                    tempSpace += 0xFF;
                                else if (testCase == 0x64)
                                    tempSpace *= 2;
                                comboBoxIntroPKMN.SelectedIndex = tempSpace;
                            }
                            else if (gameType == "E")
                            {
                                comboBoxIntroPKMN.Items.Clear();
                                for (uint i = 0; i <= numberOfPokemon; i++)
                                {
                                    comboBoxIntroPKMN.Items.Add(ROMCharactersToString(10, (uint)(0xB * i + pokemonNamesLocation)));
                                }

                                br.BaseStream.Seek(introPokemonNumber1, SeekOrigin.Begin);
                                comboBoxIntroPKMN.SelectedIndex = br.ReadInt32();
                            }
                            
                            if (!posUnlocked)
                            {
                                textBoxXPos.Visible = false;
                                textBoxYPos.Visible = false;
                                buttonXYPosUnlock.Visible = true;
                                br.BaseStream.Seek(startingPosition + 0x2, SeekOrigin.Begin);
                                textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(startingPosition + 0x4, SeekOrigin.Begin);
                                textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                            }
                            else
                            {
                                textBoxXPos.Visible = true;
                                textBoxYPos.Visible = true;
                                buttonXYPosUnlock.Visible = false;
                                br.BaseStream.Seek(startingPosition + 0x4, SeekOrigin.Begin);
                                textBoxMapBank.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(startingPosition + 0x6, SeekOrigin.Begin);
                                textBoxMap.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(startingPosition + 0x8, SeekOrigin.Begin);
                                textBoxXPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                                br.BaseStream.Seek(startingPosition, SeekOrigin.Begin);
                                textBoxYPos.Text = Convert.ToString(br.ReadByte(), 0x10);
                            }

                            comboBoxPCItemID.Items.Clear();
                            comboBoxPCItemID.Items.Add("No item");
                            for (uint i = 1; i <= numberOfItems; i++)
                            {
                                string name = ROMCharactersToString(13, 0x2C * i + itemTable);
                                comboBoxPCItemID.Items.Add(name);
                            }

                            br.BaseStream.Seek(startingPCItem, SeekOrigin.Begin);
                            int itemTester = br.ReadUInt16();
                            if (itemTester <= numberOfItems)
                                comboBoxPCItemID.SelectedIndex = itemTester;
                            else
                                comboBoxPCItemID.SelectedIndex = numberOfItems;
                            textBoxPCItemAmt.Text = Convert.ToString(br.ReadUInt16());

                            if (comboBoxPCItemID.SelectedIndex == 0)
                                textBoxPCItemAmt.Text = "0";

                            br.BaseStream.Seek(startingMoney, SeekOrigin.Begin);
                            uint moneyTester = br.ReadUInt32();
                            if (moneyTester <= 999999)
                                textBoxMoney.Text = Convert.ToString(moneyTester);
                            else
                                textBoxMoney.Text = "999999";

                            br.BaseStream.Seek(professorMusic, SeekOrigin.Begin);
                            tempSpace = br.ReadByte();
                            br.BaseStream.Seek(professorMusic + 2, SeekOrigin.Begin);
                            testCase = br.ReadByte();
                            if (testCase == 0xFF)
                                tempSpace += 0xFF;
                            else if (testCase == 0x40)
                                tempSpace *= 2;
                            if (tempSpace <= 0x1FE)
                                textBoxProfMusic.Text = Convert.ToString(tempSpace, 0x10);
                            else
                                textBoxProfMusic.Text = "1FE";

                            enableEverything();
                        }
                    }
                    else
                    {
                        disableEverything();
                        MessageBox.Show("Unsupported ROM loaded. Please load a 3rd generation Pokémon game with an existing entry in the INI file.","Unsupported ROM",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Could not open the ROM.\nCheck to see if the file is open in another program.\n\n" + ex.Message, "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(currentROM)))
                {
                    if (!posUnlocked)
                    {
                        bw.Seek(Convert.ToInt32(startingPosition + 2), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                        bw.Seek(Convert.ToInt32(startingPosition + 4), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                    }
                    else
                    {
                        bw.Seek(Convert.ToInt32(startingPosition + 4), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMapBank.Text, 0x10));
                        bw.Seek(Convert.ToInt32(startingPosition + 6), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxMap.Text, 0x10));
                        bw.Seek(Convert.ToInt32(startingPosition + 8), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxXPos.Text, 0x10));
                        bw.Seek(Convert.ToInt32(startingPosition), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(textBoxYPos.Text, 0x10));
                    }

                    if (gameType == "RS")
                    {
                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderTester);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt16)0xE04A);
                            bw.Seek(Convert.ToInt32(skipGender3), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderTester);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderOriginal);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x2001);
                            bw.Seek(Convert.ToInt32(skipGender3), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderOriginal);
                        }

                        uint pkmnPictureOffset = (uint)(pkmnPictureTable + (comboBoxIntroPKMN.SelectedIndex * 8));
                        uint pkmnPaletteOffset = (uint)(pkmnPaletteTable + (comboBoxIntroPKMN.SelectedIndex * 8));

                        bw.Seek(Convert.ToInt32(introPokemonPicture), SeekOrigin.Begin);
                        bw.Write((UInt32)pkmnPictureOffset);
                        bw.Seek(Convert.ToInt32(introPokemonPalette), SeekOrigin.Begin);
                        bw.Write((UInt32)pkmnPaletteOffset);

                        UInt16 introPKMN = (UInt16)comboBoxIntroPKMN.SelectedIndex;

                        if (introPKMN > 0xFF)
                        {
                            introPKMN -= 0xFF;
                            bw.Seek(Convert.ToInt32(introPokemonNumber1 + 2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x34FF);
                            bw.Seek(Convert.ToInt32(introPokemonNumber3 + 2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(introPokemonNumber1 + 2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x3400);
                            bw.Seek(Convert.ToInt32(introPokemonNumber3 + 2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }

                        bw.Seek(Convert.ToInt32(introPokemonNumber1), SeekOrigin.Begin);
                        bw.Write((byte)introPKMN);
                        bw.Seek(Convert.ToInt32(introPokemonNumber3), SeekOrigin.Begin);
                        bw.Write((byte)introPKMN);

                        bw.Seek(Convert.ToInt32(titlescreenMusic), SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxTitleMusic.Text, 0x10));
                    }
                    else if (gameType == "FR" | gameType == "LG")
                    {

                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((byte)0x00);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt16)0xE00A);
                            bw.Seek(Convert.ToInt32(skipGender3), SeekOrigin.Begin);
                            bw.Write((UInt16)0xE05C);
                            bw.Seek(Convert.ToInt32(skipGender4), SeekOrigin.Begin);
                            bw.Write((UInt16)0x2100);
                            bw.Write((UInt16)0xE00C);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((byte)0x30);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt16)0x2000);
                            bw.Seek(Convert.ToInt32(skipGender3), SeekOrigin.Begin);
                            bw.Write((UInt16)0x8C6C);
                            bw.Seek(Convert.ToInt32(skipGender4), SeekOrigin.Begin);
                            bw.Write((UInt16)0xF7DF);
                            bw.Write((UInt16)0xFD2B);
                        }

                        bw.Seek(Convert.ToInt32(flashbackRemove), SeekOrigin.Begin);
                        if (checkBoxFlashback.Checked == true)
                            bw.Write(new byte[] { 0x00, 0x1C, 0x0F, 0xE0 });
                        else
                            bw.Write(new byte[] { 0x00, 0x28, 0x0F, 0xD0 });

                        bw.Seek(Convert.ToInt32(rivalNamingRemove), SeekOrigin.Begin);
                        if (checkBoxSkipRivalNaming.Checked == true)
                        {
                            bw.Write(new byte[] { 0x00, 0x28, 0x17, 0xD0, 0xC0, 0x46, 0xC0, 0x46, 0x90, 0x1C });
                            bw.Seek(0x6, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x02, 0x38 });
                            bw.Seek(0x8, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x01, 0x22 });
                            bw.Seek(0x18, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x61, 0x80, 0x00, 0xF0, 0x8B, 0xF8 });
                            bw.Seek(0x16, SeekOrigin.Current);
                            if (gameType == "FR")
                                bw.Write((UInt32)0x08130859);
                            else
                                bw.Write((UInt32)0x08130831);
                            bw.Seek(0xFC, SeekOrigin.Current);
                            bw.Write(new byte[] {
                                0x06, 0x49, 0x09, 0x68, 0x06, 0x48, 0x09, 0x18,
                                0x06, 0x48, 0x02, 0x78, 0xFF, 0x2A, 0x03, 0xD0,
                                0x0A, 0x70, 0x01, 0x30, 0x01, 0x31, 0xF8, 0xE7, 
                                0x0A, 0x70, 0x70, 0x47, 0x08, 0x50, 0x00, 0x03,
                                0x4C, 0x3A, 0x00, 0x00});
                            if (gameType == "FR")
                                bw.Write((UInt32)0x081C5758);
                            else
                                bw.Write((UInt32)0x081C573A);
                            bw.Write(new byte[] {
                                0x10, 0xB5, 0x0B, 0x48, 0x01, 0x90, 0x0B, 0x49,
                                0x09, 0x68, 0x0B, 0x48, 0x09, 0x18, 0x0B, 0x4A,
                                0x0B, 0x1C, 0x10, 0x78, 0x18, 0x70, 0xFF, 0x28,
                                0x02, 0xD0, 0x01, 0x32, 0x01, 0x33, 0xF8, 0xE7,
                                0x04, 0x20, 0x00, 0x22, 0x00, 0x23, 0x6D, 0xF7, });
                            if (gameType == "FR")
                                bw.Write((byte)0xEF);
                            else
                                bw.Write((byte)0xED);
                            bw.Write(new byte[] {
                                0xFA, 0x10, 0xBC, 0x01, 0xBC, 0x00, 0x47,
                                0xE1, 0x68, 0x05, 0x08, 0x08, 0x50, 0x00, 0x03,
                                0x4C, 0x3A, 0x00, 0x00});
                            if (gameType == "FR")
                                bw.Write((UInt32)0x081C5758);
                            else
                                bw.Write((UInt32)0x081C573A);
                            bw.Seek(0x20E, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x01, 0x48, 0x30, 0x60, 0x4E, 0xE0 });
                            if (gameType == "FR")
                                bw.Write((UInt32)0x081301B1);
                            else
                                bw.Write((UInt32)0x08130189);
                            bw.Seek(0x2C6, SeekOrigin.Current);
                            bw.Write(new byte[] { 0xE0, 0x02 });

                            string addr1;
                            string addr2;
                            if (gameType == "FR")
                            {
                                addr1 = "0x13034D";
                                addr2 = "0x1C5758";
                            }
                            else
                            {
                                addr1 = "0x130325";
                                addr2 = "0x1C573A";
                            }
                            MessageBox.Show("The rival naming sequence has been removed.\nTo name the rival from within the game, use \"callasm " + addr1 + "\".\nIf you want to change the default name, change the name at " + addr2 + ", or repoint it if you want.\nNote: If you repoint the name and save your ROM in GBA Intro Manager at a later date, you'll need to go back and repoint it again.", "Skip Rival Naming", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            bw.Write(new byte[] { 0x3C, 0x21, 0x49, 0x42, 0x88, 0x42, 0x11, 0xDD, 0x90, 0x1E });
                            bw.Seek(0x6, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x02, 0x30 });
                            bw.Seek(0x8, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x02, 0x22 });
                            bw.Seek(0x18, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x28, 0x1C, 0x01, 0xF0, 0x29, 0xFA });
                            bw.Seek(0x16, SeekOrigin.Current);
                            if (gameType == "FR")
                                bw.Write((UInt32)0x08130325);
                            else
                                bw.Write((UInt32)0x081302FD);
                            bw.Seek(0xFC, SeekOrigin.Current);
                            if (gameType == "FR")
                                bw.Write(new byte[] {
                                	0xF0, 0xB5, 0x81, 0xB0, 0x00, 0x06, 0x00, 0x0E,
                                    0x81, 0x00, 0x09, 0x18, 0xCE, 0x00, 0x12, 0x4F,
                                    0xF5, 0x19, 0xDF, 0xF7, 0x2F, 0xFB, 0x00, 0x06,
                                    0x04, 0x16, 0x00, 0x2C, 0x22, 0xD0, 0x00, 0x2C,
                                    0x30, 0xDD, 0x04, 0x2C, 0x2E, 0xDC, 0x05, 0x20,
                                    0x41, 0xF7, 0xBE, 0xFF, 0xA8, 0x7E, 0x01, 0x21,
                                    0xDF, 0xF7, 0xC0, 0xF8, 0xA8, 0x7E, 0xD3, 0xF6,
                                    0x6F, 0xFD, 0x08, 0x48, 0x00, 0x68, 0x00, 0x7C,
                                    0x61, 0x1E, 0x09, 0x06, 0x09, 0x0E, 0x01, 0xF0,
                                    0xF3, 0xF9, 0x01, 0x20, 0xE8, 0x83, 0x38, 0x1C,
                                    0x08, 0x38, 0x30, 0x18, 0x02, 0x49, 0x14, 0xE0,
                                    0x98, 0x50, 0x00, 0x03, 0x08, 0xB1, 0x03, 0x02,
                                    0x65, 0x04, 0x13, 0x08, 0x05, 0x20, 0x41, 0xF7 });
                            else
                                bw.Write(new byte[] {
                                	0xF0, 0xB5, 0x81, 0xB0, 0x00, 0x06, 0x00, 0x0E,
                                    0x81, 0x00, 0x09, 0x18, 0xCE, 0x00, 0x12, 0x4F,
                                    0xF5, 0x19, 0xDF, 0xF7, 0x2F, 0xFB, 0x00, 0x06,
                                    0x04, 0x16, 0x00, 0x2C, 0x22, 0xD0, 0x00, 0x2C,
                                    0x30, 0xDD, 0x04, 0x2C, 0x2E, 0xDC, 0x05, 0x20,
                                    0x41, 0xF7, 0xD2, 0xFF, 0xA8, 0x7E, 0x01, 0x21,
                                    0xDF, 0xF7, 0xC0, 0xF8, 0xA8, 0x7E, 0xD3, 0xF6,
                                    0x83, 0xFD, 0x08, 0x48, 0x00, 0x68, 0x00, 0x7C,
                                    0x61, 0x1E, 0x09, 0x06, 0x09, 0x0E, 0x01, 0xF0,
                                    0xF3, 0xF9, 0x01, 0x20, 0xE8, 0x83, 0x38, 0x1C,
                                    0x08, 0x38, 0x30, 0x18, 0x02, 0x49, 0x14, 0xE0,
                                    0x98, 0x50, 0x00, 0x03, 0x08, 0xB1, 0x03, 0x02,
                                    0x3D, 0x04, 0x13, 0x08, 0x05, 0x20, 0x41, 0xF7 });
                            bw.Seek(0x20E, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x00, 0x20, 0x01, 0x21, 0xC6, 0xF7, 0xD9, 0xFC, 0x20, 0x1C });
                            bw.Seek(0x2C6, SeekOrigin.Current);
                            bw.Write(new byte[] { 0x0C, 0x21 });
                        }

                        int cryNumber = comboBoxCry.SelectedIndex;
                        if (cryNumber > 0xFF)
                        {
                            cryNumber -= 0xFF;
                            byte[] cryPatch = { 0xC0, 0x79, 0x80, 0x21, 0x01, 0x40, 0x09, 0x06, 0x0D, 0x0E };
                            bw.Seek(Convert.ToInt32(titlescreenCry) - 0xE, SeekOrigin.Begin);
                            bw.Write((byte[])cryPatch);
                            bw.Seek(Convert.ToInt32(titlescreenCry) + 0x2, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(titlescreenCry) + 0x2, SeekOrigin.Begin);
                            bw.Write((byte)0x00);
                        }

                        bw.Seek(Convert.ToInt32(titlescreenCry), SeekOrigin.Begin);
                        bw.Write((byte)cryNumber);

                        uint pkmnPictureOffset = (uint)(pkmnPictureTable + (comboBoxIntroPKMN.SelectedIndex * 8));
                        uint pkmnPaletteOffset = (uint)(pkmnPaletteTable + (comboBoxIntroPKMN.SelectedIndex * 8));

                        bw.Seek(Convert.ToInt32(introPokemonPicture), SeekOrigin.Begin);
                        bw.Write((UInt32)pkmnPictureOffset);
                        bw.Write((UInt32)pkmnPaletteOffset);

                        bw.Seek(Convert.ToInt32(introPokemonNumber1), SeekOrigin.Begin);
                        byte[] introPKMNFix = { Convert.ToByte(comboBoxIntroPKMN.SelectedIndex), 0x20, 0x00, 0x21 };
                        bw.Write((byte[])introPKMNFix);

                        bw.Seek(Convert.ToInt32(introPokemonNumber2), SeekOrigin.Begin);
                        bw.Write((byte)Convert.ToByte(comboBoxIntroPKMN.SelectedIndex));

                        UInt16 titleMusic = Convert.ToUInt16(textBoxTitleMusic.Text, 0x10);
                        if (titleMusic > 0xFF)
                        {
                            titleMusic -= 0xFF;
                            bw.Seek(Convert.ToInt32(titlescreenMusic) + 2, SeekOrigin.Begin);
                            bw.Write((UInt16)0x30FF);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(titlescreenMusic) + 2, SeekOrigin.Begin);
                            bw.Write((UInt16)0x3000);
                        }
                        bw.Seek(Convert.ToInt32(titlescreenMusic), SeekOrigin.Begin);
                        bw.Write((byte)titleMusic);

                        bw.Seek(Convert.ToInt32(titlescreenTime), SeekOrigin.Begin);
                        bw.Write((UInt32)(Convert.ToUInt32(textBoxSecsOnTitle.Text) * 60) - 1);
                    }
                    else if (gameType == "E")
                    {
                        if (checkBoxSkipGender.Checked == true)
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderTester);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderTester);
                        }
                        else
                        {
                            bw.Seek(Convert.ToInt32(skipGender1), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderOriginal);
                            bw.Seek(Convert.ToInt32(skipGender2), SeekOrigin.Begin);
                            bw.Write((UInt32)skipGenderOriginal);
                        }

                        bw.Seek(Convert.ToInt32(introPokemonNumber1), SeekOrigin.Begin);
                        bw.Write((UInt32)comboBoxIntroPKMN.SelectedIndex);
                        bw.Seek(Convert.ToInt32(introPokemonNumber2), SeekOrigin.Begin);
                        bw.Write((UInt32)comboBoxIntroPKMN.SelectedIndex);

                        bw.Seek(Convert.ToInt32(titlescreenMusic), SeekOrigin.Begin);
                        bw.Write((UInt32)Convert.ToUInt32(textBoxTitleMusic.Text, 0x10));

                        bw.Seek(Convert.ToInt32(introPokemonNumber1), SeekOrigin.Begin);
                        bw.Write((UInt32)comboBoxIntroPKMN.SelectedIndex);
                        bw.Seek(Convert.ToInt32(introPokemonNumber2), SeekOrigin.Begin);
                        bw.Write((UInt32)comboBoxIntroPKMN.SelectedIndex);
                    }

                    bw.Seek(Convert.ToInt32(startingPCItem), SeekOrigin.Begin);
                    bw.Write((UInt16)Convert.ToUInt16(comboBoxPCItemID.SelectedIndex));
                    bw.Write((UInt16)Convert.ToUInt32(textBoxPCItemAmt.Text));

                    bw.Seek(Convert.ToInt32(startingMoney), SeekOrigin.Begin);
                    bw.Write((UInt32)Convert.ToUInt32(textBoxMoney.Text));

                    UInt16 profMusic = Convert.ToUInt16(textBoxProfMusic.Text, 0x10);
                    bw.Seek(Convert.ToInt32(professorMusic + 2), SeekOrigin.Begin);

                    if (profMusic > 0xFF)
                    {
                        profMusic -= 0xFF;
                        bw.Write((UInt16)0x30FF);
                    }
                    else
                    {
                        bw.Write((UInt16)0x3000);
                    }
                    bw.Seek(Convert.ToInt32(professorMusic), SeekOrigin.Begin);
                    bw.Write((byte)profMusic);

                }
                MessageBox.Show("File saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Could not save ROM file.\nCheck to see if the file is open in another program.\n\n" + ex.Message, "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void ParseINI(string[] iniFile, string romCode)
        {
            bool getValues = false;
            foreach (string s in iniFile)
            {
                if (s.Equals("[" + romCode + "]"))
                {
                    getValues = true;
                    continue;
                }
                if (getValues)
                {
                    if (s.Equals(@"[/" + romCode + "]"))
                    {
                        break;
                    }
                    else
                    {
                        if (s.StartsWith("GameName"))
                        {
                            gameName = s.Split('=')[1];
                        }
                        if (s.StartsWith("GameType"))
                        {
                            gameType = s.Split('=')[1];
                        }
                        if (s.StartsWith("ItemData"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out itemTable);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out itemTable);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the item names location.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("NumberOfItems"))
                        {
                            bool success = UInt16.TryParse(s.Split('=')[1], out numberOfItems);
                            if (!success)
                            {
                                success = UInt16.TryParse(ToDecimal(s.Split('=')[1]), out numberOfItems);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the number of items.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("PokemonNames"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out pokemonNamesLocation);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out pokemonNamesLocation);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the Pokémon names offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("NumberOfPokemon"))
                        {
                            bool success = UInt16.TryParse(s.Split('=')[1], out numberOfPokemon);
                            if (!success)
                            {
                                success = UInt16.TryParse(ToDecimal(s.Split('=')[1]), out numberOfPokemon);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the number of Pokémon.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("PokemonImageTable"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out pkmnPictureTable);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out pkmnPictureTable);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the Pokémon image table offset.");
                                    break;
                                }
                            }
                            pkmnPictureTable += 0x8000000;
                        }
                        else if (s.StartsWith("PokemonPaletteTable"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out pkmnPaletteTable);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out pkmnPaletteTable);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the Pokémon palette table offset.");
                                    break;
                                }
                            }
                            pkmnPaletteTable += 0x8000000;
                        }
                        else if (s.StartsWith("StartingPosition"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out startingPosition);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out startingPosition);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the starting position offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("IntroPokemonNumber1"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out introPokemonNumber1);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out introPokemonNumber1);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the intro Pokémon's first offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("IntroPokemonNumber2"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out introPokemonNumber2);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out introPokemonNumber2);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the intro Pokémon's second offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("IntroPokemonNumber3"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out introPokemonNumber3);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out introPokemonNumber3);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the intro Pokémon's third number.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("IntroPokemonPicture"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out introPokemonPicture);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out introPokemonPicture);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the intro Pokémon picture.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("IntroPokemonPalette"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out introPokemonPalette);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out introPokemonPalette);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the intro Pokémon palette.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("TitlescreenCry"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out titlescreenCry);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out titlescreenCry);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the titlescreen cry offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("StartingPCItem"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out startingPCItem);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out startingPCItem);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the starting PC item offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("StartingMoney"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out startingMoney);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out startingMoney);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the starting money offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("TitlescreenMusic"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out titlescreenMusic);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out titlescreenMusic);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the titlescreen music offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("ProfessorMusic"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out professorMusic);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out professorMusic);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the professor music offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("TitlescreenTime"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out titlescreenTime);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out titlescreenTime);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the titlescreen time offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("SkipGender1"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGender1);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGender1);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the first gender skip offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("SkipGenderTester"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGenderTester);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGenderTester);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the first gender skip tester offset.");
                                    break;
                                }
                            }
                            skipGenderTester += 0x8000000;
                        }
                        else if (s.StartsWith("SkipGenderOriginal"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGenderOriginal);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGenderOriginal);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the original gender skip offset.");
                                    break;
                                }
                            }
                            skipGenderOriginal += 0x8000000;
                        }
                        else if (s.StartsWith("SkipGender2"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGender2);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGender2);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the second gender skip offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("SkipGender3"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGender3);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGender3);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the third gender skip offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("SkipGender4"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out skipGender4);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out skipGender4);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the fourth gender skip offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("TruckRemovedAddr"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out truckRemovedAddr);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out truckRemovedAddr);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the truck removal offset.");
                                    break;
                                }
                            }
                            truckRemovedAddr += 0x8000000;
                        }
                        else if (s.StartsWith("TruckRemovedTester"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out truckRemovedTester);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out truckRemovedTester);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the truck removal tester offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("UnlockedTester"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out unlockedTester);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out unlockedTester);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the unlock tester offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("FlashbackRemove"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out flashbackRemove);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out flashbackRemove);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the flashback removal offset.");
                                    break;
                                }
                            }
                        }
                        else if (s.StartsWith("RivalNamingRemove"))
                        {
                            bool success = UInt32.TryParse(s.Split('=')[1], out rivalNamingRemove);
                            if (!success)
                            {
                                success = UInt32.TryParse(ToDecimal(s.Split('=')[1]), out rivalNamingRemove);
                                if (!success)
                                {
                                    MessageBox.Show("There was an error parsing the value for the rival naming removal offset.");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (!getValues)
            {
                gameCode = "Unknown";
                gameName = "Unknown ROM";
            }
        }

        public string ToDecimal(string input)
        {
            if (input.ToLower().StartsWith("0x") || input.ToUpper().StartsWith("&H"))
            {
                return Convert.ToUInt32(input.Substring(2), 16).ToString();
            }
            else if (input.ToLower().StartsWith("0o"))
            {
                return Convert.ToUInt32(input.Substring(2), 8).ToString();
            }
            else if (input.ToLower().StartsWith("0b"))
            {
                return Convert.ToUInt32(input.Substring(2), 2).ToString();
            }
            else if (input.ToLower().StartsWith("0t"))
            {
                return ThornalToDecimal(input.Substring(2));
            }
            else if ((input.StartsWith("[") && input.EndsWith("]")) || (input.StartsWith("{") && input.EndsWith("}")))
            {
                return Convert.ToUInt32(input.Substring(1, (input.Length - 2)), 2).ToString();
            }
            else if (input.ToLower().EndsWith("h"))
            {
                return Convert.ToUInt32(input.Substring(0, (input.Length - 1)), 16).ToString();
            }
            else if (input.ToLower().EndsWith("b"))
            {
                return Convert.ToUInt32(input.Substring(0, (input.Length - 1)), 2).ToString();
            }
            else if (input.ToLower().EndsWith("t"))
            {
                return ThornalToDecimal(input.Substring(0, (input.Length - 1)));
            }
            else if (input.StartsWith("$"))
            {
                return Convert.ToUInt32(input.Substring(1), 16).ToString();
            }
            else
            {
                return Convert.ToUInt32(input, 16).ToString();
            }
        }

        private string ThornalToDecimal(string input)
        {
            uint total = 0;
            char[] temp = input.ToCharArray();
            for (int i = input.Length - 1; i >= 0; i--)
            {
                int value = 0;
                bool success = Int32.TryParse(temp[i].ToString(), out value);
                if (!success)
                {
                    if (temp[i] < 'W' && temp[i] >= 'A')
                    {
                        value = temp[i] - 'A' + 10;
                    }
                    else
                    {
                        throw new FormatException(temp[i] + " is an invalid character in the Base 32 number set.");
                    }
                }
                total += (uint)(Math.Pow((double)32, (double)(input.Length - 1 - i)) * value);
            }
            return total.ToString();
        }

        private string ROMCharactersToString(int maxLength, uint baseLocation)
        {
            string s = "";
            using (BinaryReader br = new BinaryReader(File.OpenRead(currentROM)))
            {
                for (int j = 0; j < maxLength; j++)
                {
                    br.BaseStream.Seek(baseLocation + j, SeekOrigin.Begin);
                    byte textByte = br.ReadByte();
                    if ((textByte != 0xFF))
                    {
                        char temp = ';';
                        bool success = characterValues.TryGetValue(textByte, out temp);
                        s += temp;
                        if (!success)
                        {
                            if (textByte == 0x53)
                            {
                                s = s.Substring(0, s.Length - 1) + "PK";
                            }
                            else if (textByte == 0x54)
                            {
                                s = s.Substring(0, s.Length - 1) + "MN";
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return s;
        }

        private Dictionary<byte, char> ReadTableFile(string iniLocation)
        {
            Dictionary<byte, char> characterValues = new Dictionary<byte, char>();
            try
            {
                string[] tableFile = System.IO.File.ReadAllLines(iniLocation);
                int index = 0;
                foreach (string s in tableFile)
                {
                    if (!s.Equals("") && !s.Equals("[Table]") && index != 0x9E && index != 0x9F)
                    {
                        string[] stuff = s.Split('=');
                        switch (Byte.Parse(ToDecimal("0x" + stuff[0])))
                        {
                            case 0:
                                characterValues.Add(0, ' ');
                                break;
                            case 0x34:
                                break;
                            case 0x35:
                                characterValues.Add(0x35, '=');
                                break;
                            case 0x53:
                                break;
                            case 0x54:
                                break;
                            case 0x55:
                                break;
                            case 0x56:
                                break;
                            case 0x57:
                                break;
                            case 0x58:
                                break;
                            case 0x59:
                                break;
                            case 0x79:
                                break;
                            case 0x7A:
                                break;
                            case 0x7B:
                                break;
                            case 0x7C:
                                break;
                            case 0xB0:
                                break;
                            case 0xEF:
                                break;
                            case 0xF7:
                                break;
                            case 0xF8:
                                break;
                            case 0xF9:
                                break;
                            case 0xFA:
                                break;
                            case 0xFB:
                                break;
                            case 0xFC:
                                break;
                            case 0xFD:
                                break;
                            case 0xFE:
                                break;
                            case 0xFF:
                                break;
                            default:
                                characterValues.Add(Byte.Parse(ToDecimal("0x" + stuff[0])), stuff[1].ToCharArray()[0]);
                                break;
                        }
                        index++;
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Could not open Table.ini. Did you delete it?\n\n" + ex.Message, "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            return characterValues;
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
            comboBoxCry.Enabled = true;
            comboBoxIntroPKMN.Enabled = true;
            textBoxTitleMusic.Enabled = true;
            textBoxProfMusic.Enabled = true;
            comboBoxPCItemID.Enabled = true;
            textBoxMoney.Enabled = true;
            checkBoxSkipGender.Enabled = true;

            if (comboBoxPCItemID.SelectedIndex == 0)
                textBoxPCItemAmt.Enabled = false;
            else
                textBoxPCItemAmt.Enabled = true;
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
            comboBoxCry.Enabled = false;
            comboBoxCry.Items.Clear();
            comboBoxIntroPKMN.Enabled = false;
            comboBoxIntroPKMN.ResetText();
            textBoxTitleMusic.Enabled = false;
            textBoxTitleMusic.ResetText();
            textBoxProfMusic.Enabled = false;
            textBoxProfMusic.ResetText();
            comboBoxPCItemID.Enabled = false;
            textBoxPCItemAmt.Enabled = false;
            textBoxPCItemAmt.ResetText();
            textBoxMoney.Enabled = false;
            textBoxMoney.ResetText();
            textBoxSecsOnTitle.Enabled = false;
            textBoxSecsOnTitle.ResetText();
            buttonResetSecsOnTitle.Enabled = false;
            checkBoxFlashback.Enabled = false;
            checkBoxFlashback.Checked = false;
            checkBoxSkipGender.Enabled = false;
            checkBoxSkipGender.Checked = false;
            checkBoxSkipRivalNaming.Enabled = false;
            checkBoxSkipRivalNaming.Checked = false;
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
            if (gameType == "RS" || gameType == "E")
            {
                textBoxMapBank.Text = "19";
                textBoxMap.Text = "28";
                textBoxXPos.Text = "2";
                textBoxYPos.Text = "2";
            }
            else
            {
                textBoxMapBank.Text = "4";
                textBoxMap.Text = "1";
                textBoxXPos.Text = "6";
                textBoxYPos.Text = "6";
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GBA Intro Manager v0.1.2\nCreated by Diegoisawesome.\n\nThanks to:\nJambo51\ncolcolstyles\nxGal", "About");
        }

        private void btnReadme_Click(object sender, EventArgs e)
        {
            try
            {
                string fileLoc = Application.StartupPath + "/Readme.txt";
                Process.Start(fileLoc);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Could not open Readme.txt. Did you delete it?\n\n" + ex.Message, "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonXYPosUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                byte originalMapBank;
                byte originalMap;
                byte[] introPatch;

                using (BinaryReader br = new BinaryReader(File.OpenRead(currentROM)))
                {
                    br.BaseStream.Seek(Convert.ToInt32(startingPosition) + 2, SeekOrigin.Begin);
                    originalMapBank = br.ReadByte();
                    br.BaseStream.Seek(Convert.ToInt32(startingPosition) + 4, SeekOrigin.Begin);
                    originalMap = br.ReadByte();
                }

                if (gameType == "RS")
                    introPatch = new byte[] { 0x01, 0x22, 0x52, 0x42, 0x02, 0x20, 0x00, 0x90, originalMapBank, 0x20, originalMap, 0x21, 0x02, 0x23, 0x00, 0xF0, 0x1D, 0xFB, 0x00, 0xF0, 0x11, 0xFB, 0x01, 0xB0, 0x01, 0xBC, 0x00, 0x47 };
                else
                    introPatch = new byte[] { 0x01, 0x22, 0x52, 0x42, 0x02, 0x20, 0x00, 0x90, originalMapBank, 0x20, originalMap, 0x21, 0x02, 0x23, 0x00, 0xF0, 0xC5, 0xFB, 0x00, 0xF0, 0xB9, 0xFB, 0x01, 0xB0, 0x01, 0xBC, 0x00, 0x47 };

                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(currentROM)))
                {

                    bw.Seek(Convert.ToInt32(startingPosition) - 4, SeekOrigin.Begin);
                    bw.Write((byte[])introPatch);
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
                MessageBox.Show("Could not write to ROM file.\nCheck to see if the file is open in another program.", "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxCry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonTruckRemove_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(currentROM)))
                {
                    bw.Seek((Int32)truckRemovedTester, SeekOrigin.Begin);
                    bw.Write((Int32)truckRemovedAddr);
                }
                buttonTruckRemove.Text = "Truck Removed";
                buttonTruckRemove.Enabled = false;
            }
            catch (IOException)
            {
                MessageBox.Show("Could not write to ROM file.\nCheck to see if the file is open in another program.", "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonResetCry_Click(object sender, EventArgs e)
        {
            if (gameType == "FR")
                comboBoxCry.SelectedIndex = 0x6;
            else
                comboBoxCry.SelectedIndex = 0x3;
        }

        private void buttonResetIntroPKMN_Click(object sender, EventArgs e)
        {
            if (gameType == "RS")
                comboBoxIntroPKMN.SelectedIndex = 0x15E;
            else if (gameType == "FR" | gameType == "LG")
                comboBoxIntroPKMN.SelectedIndex = 0x1D;
            else
                comboBoxIntroPKMN.SelectedIndex = 0x127;
        }

        private void buttonResetStartItems_Click(object sender, EventArgs e)
        {
            comboBoxPCItemID.SelectedIndex = 0xD;
            textBoxPCItemAmt.Text = "1";
            textBoxMoney.Text = "3000";
        }

        private void buttonResetMusic_Click(object sender, EventArgs e)
        {
            if (gameType == "RS")
            {
                textBoxTitleMusic.Text = "19D";
                textBoxProfMusic.Text = "176";
            }
            else if (gameType == "FR" | gameType == "LG")
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
            else if ((n > 0x1FE) && (gameType == "FR" | gameType == "LG"))
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

        private void comboBoxPCItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPCItemID.SelectedIndex == 0)
            {
                textBoxPCItemAmt.Enabled = false;
                textBoxPCItemAmt.Text = "0";
            }
            else
                textBoxPCItemAmt.Enabled = true;
        }

        private void buttonResetSecsOnTitle_Click(object sender, EventArgs e)
        {
            textBoxSecsOnTitle.Text = "45";
        }

        private void linkLabelDoMoreAwesome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://domoreaweso.me");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Could not open the link.\n\n" + ex.Message, "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
