using System;
using System.Windows.Forms;
using SampleClassification.Model;

namespace TryMachineLearningWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void feelBtn_Click(object sender, EventArgs e)
        {
            var input = new ModelInput { Col0 = sentenceTxtBx.Text };
            var predictionResult = ConsumeModel.Predict(input);
            var chanceOfBad = predictionResult.Score[0];
            var chanceOfGood = predictionResult.Score[1];
            messageLbl.Text = chanceOfBad > chanceOfGood
                ? $"I'm sure {chanceOfBad * 100f:0.##}% I feel bad 🙁 about that sentence."
                : $"I'm sure {chanceOfGood * 100f:0.##}% I feel good 🙂 about that sentence.";
        }
    }
}
