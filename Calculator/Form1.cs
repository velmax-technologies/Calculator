using System.Data;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace Calculator
{
    public partial class frmMain : Form
    {
        bool answer = true;
        string currentAnswer = string.Empty;
        bool operation = false;
        bool isOperation = false;
        string currentOperation = string.Empty;
        bool hasDot = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            clear();
        }

        #region Operations
        private void operationBtn_Click(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;
            if (operation && currentOperation != buttonText)
            {
                currentOperation = buttonText;
                lblExpression.Text = lblExpression.Text.Remove(lblExpression.Text.Length - 1, 1);
                lblExpression.Text += buttonText;
                return;
            }
            else if (operation)
            {
                return;
            }

            if (lblExpression.Text == string.Empty && answer)
            {
                lblExpression.Text = lblAnswer.Text + buttonText;
                lblAnswer.Visible = false;
                lblOutput.Text = string.Empty;
                answer = false;
                currentAnswer = string.Empty;
            }
            else
            {
                lblExpression.Text += buttonText;
            }
            operation = true;
            currentOperation = buttonText;
            hasDot = false;

        }
        #endregion

        #region Numeric Buttons
        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (answer && lblAnswer.Text == "0")
            {
                lblAnswer.Text = ((Button)sender).Text;
            }
            else if (answer && currentAnswer != string.Empty)
            {
                lblAnswer.Text = ((Button)sender).Text;
                currentAnswer = string.Empty;

            }
            else if (answer)
            {
                lblAnswer.Text += ((Button)sender).Text;
            }
            else
            {
                lblExpression.Text += ((Button)sender).Text;
            }
            operation = false;
            calculate();
        }
        #endregion

        // Dot Button
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!hasDot && answer)
            {
                lblAnswer.Text += ((Button)sender).Text;
                hasDot = true;
            }else if(!hasDot && !answer)
            {
                lblExpression.Text += ((Button)sender).Text;
                hasDot = true;
            }
        }

        #region functions
        private void clear()
        {
            lblAnswer.Text = "0";
            lblAnswer.Visible = true;
            answer = true;
            lblExpression.Text = string.Empty;
            lblOutput.Text = string.Empty;
        }

        private void calculate()
        {
            string expression = lblExpression.Text;


            if (expression.Contains("+") || expression.Contains("-") || expression.Contains("x") || expression.Contains("÷"))
            {
                DataTable dt = new DataTable();
                expression = expression.Replace('x', '*');
                expression = expression.Replace('÷', '/');
                var v = dt.Compute(expression, "");
                lblOutput.Text = v.ToString();

            }

        }
        #endregion

        // Cear Button
        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        // Clear Entry Button
        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            if (currentAnswer != string.Empty || lblExpression.Text.Length == 1)
            {
                clear();
                return;
            }
            string newInput = string.Empty;
            if (!answer)
            {
                newInput = lblExpression.Text.Remove(lblExpression.Text.Length - 1, 1);
                lblExpression.Text = newInput;
            }
            else
            {
                newInput = lblAnswer.Text.Remove(lblAnswer.Text.Length - 1, 1);
                lblAnswer.Text = newInput;
            }

            string lastCharacter = string.Empty;
            if(newInput.Length > 0)
            {
                lastCharacter = newInput.Substring(newInput.Length - 1);
            }
            else
            {
                clear();
                return;
            }
            if (lastCharacter == "+" || lastCharacter == "-" || lastCharacter == "x" || lastCharacter == "÷")
            {
                lblOutput.Text = string.Empty;
            }
            else
            {
                calculate();
            }


        }

        // Answer Button
        private void btnAns_Click(object sender, EventArgs e)
        {
            string expression = lblExpression.Text;
            if (expression.Length > 0)
            {
                char lastChar = expression.Last();
                if (lastChar == '+' || lastChar == '-' || lastChar == 'x' || lastChar == '÷')
                {
                    return;
                }
                else
                {
                    calculate();
                }
            }

            if (!answer)
            {
                lblAnswer.Text = lblOutput.Text;
                lblAnswer.Visible = true;
                lblExpression.Text = string.Empty;
                operation = false;
                answer = true;
                currentOperation = string.Empty;
                currentAnswer = lblAnswer.Text;
            }
        }

        
    }
}