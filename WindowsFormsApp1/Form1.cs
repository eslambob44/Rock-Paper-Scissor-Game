using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class frmRockPaperScissorGame : Form
    {
        public frmRockPaperScissorGame()
        {
            InitializeComponent();
        }

        enum enChoice {Rock=0,Paper=1,Scissor=2 , NoChoice };

        enum enPlayer { Player=0,Computer=1};

        enum enWinner {Player=0,Computer=1,Draw=2};

        struct stRoundResult
        {
            public enChoice PlayerChoice;
            public enChoice ComputerChoice;
            public enWinner Winner;
        };

        enChoice GetComputerChoice()
        {
            Random rand = new Random();
            return (enChoice)rand.Next(0, 3);
        }

        enChoice ChangeStringChoiceToEnum(string Choice)
        {
            switch (Choice)
            {
                case "Rock":
                    return enChoice.Rock;
                    break;
                case "Paper":
                    return enChoice.Paper;
                    break;
                case "Scissor":
                    return enChoice.Scissor;
                    break;
            }

            return enChoice.NoChoice ;
        }
        void ChangePictureBoxChoice(Button btn)
        {
            ChangePictureBoxChoice(ChangeStringChoiceToEnum(btn.Tag.ToString()), enPlayer.Player);
        }

        void ChangePictureBoxChoice(enChoice Choice ,enPlayer Player )
        {
            PictureBox pb = (Player == enPlayer.Player) ? pbPlayerChoice : pbComputerChoice;

            switch (Choice)
            {
                case enChoice.Paper:
                    pb.Image=Resources.Player_Paper;
                    break;
                case enChoice.Scissor:
                    pb.Image=Resources.Player_Scissor;
                    break;
                case enChoice.Rock:
                    pb.Image=Resources.Player_Rock;
                    break;
                case enChoice.NoChoice:
                    pb.Image = null;
                    break;
            }
        }

        enWinner GetWinner(enChoice PlayerChoice , enChoice ComputerChoice)
        {
            if(PlayerChoice==ComputerChoice)
            {
                return enWinner.Draw;
            }

            switch(PlayerChoice)
            {
                case enChoice.Rock:
                    if(ComputerChoice == enChoice.Scissor)
                    {
                        return enWinner.Player;
                    }
                    break;
                case enChoice.Paper:
                    if(ComputerChoice==enChoice.Rock)
                    {
                        return enWinner.Player;
                    }
                    break;
                case enChoice.Scissor:
                    if(ComputerChoice==enChoice.Paper)
                    {
                        return enWinner.Player;
                    }
                    break;
            }

            return enWinner.Computer;
        }

         

        stRoundResult PlayRound(enChoice PlayerChoice)
        {
            stRoundResult RoundResult = new stRoundResult();
            RoundResult.PlayerChoice = PlayerChoice;
            RoundResult.ComputerChoice = GetComputerChoice();
            RoundResult.Winner = GetWinner(RoundResult.PlayerChoice,RoundResult.ComputerChoice);
            return RoundResult;
        }

        
        void ChangeGameBtnEnabled()
        {
            btnRock.Enabled = !btnRock.Enabled;
            btnPaper.Enabled = !btnPaper.Enabled;
            btnScissor.Enabled = !btnScissor.Enabled;
            btnReset.Enabled = !btnReset.Enabled;
        }

        private void btnGame_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            
            ChangeGameBtnEnabled();
            ChangePictureBoxChoice((Button)sender);
            stRoundResult RoundResult = new stRoundResult();
            RoundResult = PlayRound(ChangeStringChoiceToEnum(btn.Tag.ToString()));
            ChangePictureBoxChoice(RoundResult.ComputerChoice,enPlayer.Computer);
            lblResult.Text = "Winner: "+ RoundResult.Winner.ToString();
            lblResult.Visible = true;


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ChangeGameBtnEnabled();
            ChangePictureBoxChoice(enChoice.NoChoice,enPlayer.Player);
            ChangePictureBoxChoice(enChoice.NoChoice, enPlayer.Computer);
            lblResult.Visible = false;
            
        }
    }
}
