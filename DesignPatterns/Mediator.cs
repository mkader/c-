using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Reflection;

namespace Mediator
{
    #region "QP Example - Mediator will help to Minimize Setting & getting object value"
    #region "Like code to clear and set"
    /*public class Invoice
    {

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public double Amount { get; set; }
        public double PaidAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceComments { get; set; }
        public string InvoiceReference { get; set; }
        public int Quantity { get; set; }
        public double TaxAmount { get; set; }
    }
    public class UI1
    {
        public TextBox txtCustomerName { get; set; }
        public TextBox txtCustomerAddress { get; set; }
        public Label lblTotalAmountToBePaid { get; set; }
        public TextBox txtAmountPaid { get; set; }
        public TextBox txtInvoiceDate { get; set; }
        public TextBox txtComments { get; set; }
        public TextBox txtInvoiceNumber { get; set; }
        public TextBox TxtQuantity { get; set; }
        public TextBox txtTaxAmount { get; set; }

        public void setObjectFromUI(Invoice objInvoice)
        {
            objInvoice.CustomerName = txtCustomerName.Text;
            objInvoice.CustomerAddress = txtCustomerAddress.Text;
            objInvoice.Amount = Convert.ToDouble(lblTotalAmountToBePaid.Text);
            objInvoice.PaidAmount = Convert.ToDouble(txtAmountPaid.Text);
            objInvoice.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            objInvoice.InvoiceComments = txtComments.Text;
            objInvoice.InvoiceReference = txtInvoiceNumber.Text;
            objInvoice.Quantity = Convert.ToInt16(TxtQuantity.Text);
            objInvoice.TaxAmount = Convert.ToDouble(txtTaxAmount.Text);
        }
        public void SetObjectToUI(Invoice objInvoice)
        {
            txtCustomerName.Text = objInvoice.CustomerName;
            txtCustomerAddress.Text = objInvoice.CustomerAddress;
            lblTotalAmountToBePaid.Text = objInvoice.Amount.ToString();
            txtAmountPaid.Text = objInvoice.PaidAmount.ToString();
            txtInvoiceDate.Text = objInvoice.InvoiceDate.ToString();
            txtComments.Text = objInvoice.InvoiceComments.ToString();
            txtInvoiceNumber.Text = objInvoice.InvoiceReference.ToString();
            TxtQuantity.Text = objInvoice.Quantity.ToString();
            txtTaxAmount.Text = objInvoice.TaxAmount.ToString();
        }

        public void clearText()
        {
            txtInvoiceNumber.Text = "";
            txtComments.Text = "";
            txtInvoiceDate.Text = "";
            TxtQuantity.Text = "";
            lblTotalAmountToBePaid.Text = "";
            txtTaxAmount.Text = "";
            txtAmountPaid.Text = "";
            txtCustomerName.Text = "";
            txtCustomerAddress.Text = "";
        }
    }*/
    #endregion "Like code to clear and set"
    public class clsSampleClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }

        public clsSampleClass()
        {
            Property1 = "Property 1";
            Property2 = "Property 2 ";
        }
    }

    public class clsMediator
    {

        private ArrayList objtextboxes = new ArrayList();
        private ArrayList objPropertyInfo = new ArrayList();

        public void Add(TextBox objtextBox, string strPropertyName)
        {
            objtextboxes.Add(objtextBox);
            objPropertyInfo.Add(strPropertyName);
        }

        public void setValuesToObject(clsSampleClass objSample)
        {
            // get the object type
            Type objType = objSample.GetType();
            // browse through all the UI object and set the object 
            // from the UI to the corresponding property
            for (int i = 0; i < objtextboxes.Count; i++)
            {
                // get the UI object
                PropertyInfo objProperty = objType.GetProperty(objPropertyInfo[i].ToString());
                // set the object property from the UI object
                objProperty.SetValue(objSample, ((TextBox)objtextboxes[i]).Text, null);
            }

        }
        public void setValuesToUI(clsSampleClass objSample)
        {

            Type objType = objSample.GetType();
            for (int i = 0; i < objtextboxes.Count; i++)
            {
                PropertyInfo objProperty = objType.GetProperty(objPropertyInfo[i].ToString());
                ((TextBox)objtextboxes[i]).Text = objProperty.GetValue(objSample, null).ToString();
            }

        }
        public void ClearTextBox()
        {
            Type objType = Type.GetType("clsSampleClass");
            for (int i = 0; i < objtextboxes.Count; i++)
            {
                ((TextBox)objtextboxes[i]).Text = "";
            }

        }
    }

    public  class UI2
    {
        private Label Label1 = new Label();
        private Label Label4 = new Label();
        private TextBox TextBox1 = new TextBox();
        private TextBox TextBox2 = new TextBox();
        private clsMediator objMediator;
        private clsSampleClass objSample = new clsSampleClass();
        public void Page_Load()
        {
            objMediator = new clsMediator();
            objMediator.Add(TextBox1, "Property1");
            objMediator.Add(TextBox2, "Property2");
            objMediator.setValuesToUI(objSample);
        }
        public void Button1_Click()
        {
            objMediator.setValuesToObject(objSample);
            Label1.Text = objSample.Property1;
            Label4.Text = objSample.Property2;
        }
        public void Button2_Click()
        {
            objMediator.ClearTextBox();
        }

    }
    [TestClass]
    public class Client2
    {
        [TestMethod]
        public void Demo1()
        {
            UI2 ui = new UI2();
            ui.Page_Load();
            ui.Button1_Click();
            ui.Button2_Click();

        }
    }

    #endregion "QP Example"

    #region "QP Example - Add, Clear, Enable button & text"
    /*
     * If type in text box, Enable Add & Clear button, 
     * if no text in text box, disabled it.
     * If Click Add button, add textbox.text to listbox, clear textbox and disable add & clear button
     * If Click Clear button, clear textbox and disable add & clear button
     * 
     * Instead of adding add all logic in the class, i added in moderator, moderator will take care it.
     */
    public class Mediator
    {
        private ListBox listBox;
        private TextBox textBox;
        private Button btnAdd;
        private Button btnClear;
        public void Register(ListBox l) {
            listBox = l;
        }
        public void Register(TextBox t) {
            textBox = t;    
        }
        public void Register(Button b) {
            if (b.Name == "btnAdd")
            {
                btnAdd = b;
            }
            else
            {
                btnClear = b;
            }
        }
        public void TextChange()
        {
            if (textBox.Text.Length > 0)
            {
                btnAdd.Enabled = true;
                btnClear.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnClear.Enabled = false;
            }
        }
        public void Add()
        {
            listBox.Items.Add(textBox.Text);
            textBox.Text = "";
            btnAdd.Enabled = false;
            btnClear.Enabled = false;
        }

        public void Clear()
        {
            textBox.Text = "";
            btnAdd.Enabled = false;
            btnClear.Enabled = false;
        }
    }

    public class UI
    {
        private Mediator m = new Mediator();
        public TextBox txtName { get; set; }
        public Button btnAdd { get; set; }
        public Button btnClear { get; set; }
        public ListBox lstName { get; set; }
        public UI()
        {
            txtName = new TextBox();
            btnAdd = new Button();
            btnClear = new Button();
            lstName = new ListBox();
            btnAdd.Name = "btnAdd";
            btnClear.Name = "btnClear";
            m.Register(txtName);
            m.Register(btnAdd);
            m.Register(btnClear);
            m.Register(lstName);
        }

        public void txtName_TextChanged()
        {
            m.TextChange();
        }

        public void btnAdd_Click()
        {
            m.Add();
        }

        public void btnClear_Click()
        {
            m.Clear();
        }
    }
    
    [TestClass]
    public  class Client1
    {
        [TestMethod]
        public  void Demo1()
        {
            UI ui = new UI();
            ui.txtName.Text = "test";
            ui.txtName_TextChanged();
            ui.btnAdd_Click();
            ui.btnClear_Click();
            ui.txtName.Text = "test2";
            ui.txtName_TextChanged();

        }
    }

    #endregion "QP Example"

    #region "TP Example - Chat Example"
    public class User
    {
        public string  Name { get; set; }

        public void Message(string msg)
        {
            ChatRoom.SendMessage(this, msg);
        }
    }

    public class ChatRoom
    {
        public static void SendMessage(User user, string msg)
        {
            Console.WriteLine(DateTime.Now + " [" + user.Name+"] : " + msg);
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            User a = new User() { Name = "A" };
            User b = new User() { Name = "B" };
            a.Message("Hi A is Chatting");
            b.Message("Hi B is Chatting");
            Console.Read();
        }
    }
    #endregion "TP Example"

}
