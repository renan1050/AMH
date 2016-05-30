using BusinessRules.DatabaseBase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InterfaceBase
{
    public class InterfaceManagement
    {        
        #region MÉTODO PARA A EXIBIÇÃO/ESCONDAÇÃO DO CAMPO CODIGO E DO CARREGAMENTO DE UM REGISTRO
        public void LoadByValue(string pValue, Window pWindow, Action<string> pLoadAction = null, string pLoadValue = null)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow,"RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        if (lUIElement.IsVisible)
                        {
                            if (!string.IsNullOrEmpty(pLoadValue))
                            {
                                if (pLoadAction != null)
                                    pLoadAction(pLoadValue);                                
                            }

                            lUIElement.Visibility = Visibility.Hidden;
                        }
                        else
                            lUIElement.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        #endregion

        #region MÉTODOS PARA MOSTRAR/ESCONDER CAMPOS COM BASE NO VALOR DE ATRIBUTOS
        public void ShowByAttribute(string pValue, Window pWindow)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        lUIElement.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public void HideByAttribute(string pValue, Window pWindow)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        lUIElement.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
        #endregion

        #region MÉTODOS PARA PEGAR OS VALORES DOS FORMS
        public object BuildDM(DependencyObject pWindow, Type pTipoObjeto, string pEnviador, List<string> pErrosValidacao)
        {
            object lRetorno = Activator.CreateInstance(pTipoObjeto);
            var lPropriedades = pTipoObjeto.GetProperties();
            List<DependencyObject> lLabels = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "Refers").ToList();
            List<Object> lChildren = new List<Object>();
            GetControlsList(pWindow, lChildren, true);
            PropertyInfo lPropriedade;
            List<Object> lParents = new List<Object>();
            bool lResult;
            foreach (Object lSender in lChildren) 
            {
                if(lSender is TextBox){
                    TextBox lTextBox = (TextBox)lSender;
                    if (!string.IsNullOrEmpty(lTextBox.Text) && lPropriedades.Select(x => x.Name).ToList().Contains(lTextBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lTextBox.Name).First();

                        Label lField = null;
                        lResult = true;

                        if (lTextBox.Name == "pesCPF")
                        {
                            lResult = ValidateCPF(lTextBox.Text);
                            lField = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault() as Label;
                        }
                        if (lTextBox.Name == "pesCNPJ")
                        {
                            lResult = ValidateCNPJ(lTextBox.Text);
                            lField = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault() as Label;
                        }
                        if (lTextBox.Name == "pesCEP")
                        {
                            lResult = ValidateCEP(lTextBox.Text);
                            lField = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault() as Label;
                        }
                        if (lTextBox.Name == "pesEmail")
                        {
                            lResult = ValidateEmail(lTextBox.Text);
                            lField = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault() as Label;
                        }
                        if (lTextBox.Name == "veiPlaca")
                        {
                            lResult = ValidatePlate(lTextBox.Text);
                            lField = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault() as Label;
                        }

                        if (!lResult)
                            pErrosValidacao.Add(string.Concat("O campo ", (lField != null ? lField.Content.ToString() : lTextBox.Name), " está incorreto."));
                        else
                            lPropriedade.SetValue(lRetorno, Database.ChangeType(lTextBox.Text, lPropriedade.PropertyType));

                        
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lTextBox);

                        if (lRequired != null && lTextBox.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault();
                            pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lTextBox.Name), " é obrigatório."));
                        }
                    }
                }
                if (lSender is DatePicker)
                {
                    DatePicker lDatePicker = (DatePicker)lSender;
                    if (!string.IsNullOrEmpty(lDatePicker.Text) && lPropriedades.Select(x => x.Name).ToList().Contains(lDatePicker.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lDatePicker.Name).First();
                        lPropriedade.SetValue(lRetorno, Database.ChangeType(lDatePicker.Text, lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lDatePicker);

                        if (lRequired != null && lDatePicker.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lDatePicker.Name)).FirstOrDefault();
                            pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lDatePicker.Name), " é obrigatório."));
                        }
                    }
                }
                else if (lSender is ComboBox)
                {
                    ComboBox lComboBox = (ComboBox)lSender;
                    string lName = string.Empty;

                    if (lComboBox.SelectedItem is InterfaceManagement.Item)
                        lName = (lComboBox.SelectedItem as InterfaceManagement.Item).Campo;
                    else
                        lName = lComboBox.Name;

                    if (lComboBox.SelectedValue != null && !string.IsNullOrEmpty(lComboBox.SelectedValue.ToString()) && lPropriedades.Select(x => x.Name).ToList().Contains(lName))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lName).First();

                        lPropriedade.SetValue(lRetorno, Database.ChangeType(lComboBox.SelectedValue.ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lComboBox);

                        if (lRequired != null && lComboBox.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lComboBox.Name)).FirstOrDefault();
                            pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lComboBox.Name), " é obrigatório."));
                        }
                    }
                }
                else if (lSender is CheckBox)
                {
                    CheckBox lCheckBox = (CheckBox)lSender;
                    DependencyProperty lValue = DependencyObjectHelper.GetDependencyPropertyByName("Value", lCheckBox);
                    if (lValue != null && lPropriedades.Select(x => x.Name).ToList().Contains(lCheckBox.Name.Split('_').FirstOrDefault()) && lCheckBox.IsChecked == true)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lCheckBox.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Database.ChangeType(lCheckBox.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lCheckBox.Parent);

                        if (lRequired != null)
                        {
                            GetControlsList(lCheckBox.Parent, lParents);
                            if (lParents.Count == 0)
                            {
                                pErrosValidacao.Add(string.Concat("O campo ", ((GroupBox)((Grid)lCheckBox.Parent).Parent).Header.ToString(), " é obrigatório."));
                            }
                        }
                    }
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lRadioButton = (RadioButton)lSender;
                    DependencyProperty lValue = DependencyObjectHelper.GetDependencyPropertyByName("Value", lRadioButton);
                    if (lValue != null && lPropriedades.Select(x => x.Name).ToList().Contains(lRadioButton.Name.Split('_').FirstOrDefault()) && lRadioButton.IsChecked == true)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lRadioButton.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Database.ChangeType(lRadioButton.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lRadioButton.Parent);

                        if (lRequired != null)
                        {
                            GetControlsList(lRadioButton.Parent, lParents);
                            if (lParents.Count == 0)
                            {
                                pErrosValidacao.Add(string.Concat("O campo ", ((GroupBox)((Grid)lRadioButton.Parent).Parent).Header.ToString(), " é obrigatório."));
                            }
                        }
                    }
                }
                
            }                

            return lRetorno;
        
        }

        public bool CarregarDM(DependencyObject pWindow, object pDM)
        {
            if (pDM == null)
            {
                MessageBox.Show("Registro não encontrado");
                return false;
            }

            var lPropriedades = pDM.GetType().GetProperties();

            List<Object> lChildren = new List<Object>();
            GetControlsList(pWindow, lChildren, true, true);
            PropertyInfo lPropriedade;
            foreach (Object lSender in lChildren)
            {
                if (lSender is TextBox)
                {
                    TextBox lTextBox = (TextBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lTextBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lTextBox.Name).First();
                        if (lPropriedade.GetValue(pDM) != null)
                            lTextBox.Text = lPropriedade.GetValue(pDM).ToString();
                        else
                            lTextBox.Text = string.Empty;
                    }
                }
                else if (lSender is DatePicker)
                {
                    DatePicker lDatePicker = (DatePicker)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lDatePicker.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lDatePicker.Name).First();
                        if (lPropriedade.GetValue(pDM) != null)
                            lDatePicker.Text = lPropriedade.GetValue(pDM).ToString();
                        else
                            lDatePicker.Text = string.Empty;
                    }
                }
                else if (lSender is ComboBox)
                {
                    ComboBox lComboBox = (ComboBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lComboBox.Name).First();
                        if (lPropriedade.GetValue(pDM) != null)
                            lComboBox.SelectedValue = lPropriedade.GetValue(pDM).ToString();
                        else
                        {
                            DependencyProperty lRefers = DependencyObjectHelper.GetDependencyPropertyByName("Refers", lComboBox);

                            if(lRefers != null)
                            {
                                string[] lRef = lComboBox.GetValue(lRefers).ToString().Split(';');
                                foreach(string lItem in lRef)
                                {
                                    lPropriedade = lPropriedades.Where(x => x.Name == lItem).First();
                                    if (lPropriedade.GetValue(pDM) != null)
                                    {
                                        lComboBox.SelectedValue = lPropriedade.GetValue(pDM).ToString();
                                        break;
                                    }
                                }
                            }
                            else
                                lComboBox.SelectedValue = string.Empty;
                        }
                    }
                }
                else if (lSender is CheckBox)
                {
                    CheckBox lCheckBox = (CheckBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lCheckBox.Name.Split('_').FirstOrDefault()))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lCheckBox.Name.Split('_').FirstOrDefault()).First();
                        if (lPropriedade.GetValue(pDM) != null && lPropriedade.GetValue(pDM).ToString() == lCheckBox.Name.Split('_').LastOrDefault())
                            lCheckBox.IsChecked = true;
                        else
                            lCheckBox.IsChecked = false;
                    }
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lComboBox = (RadioButton)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name.Split('_').FirstOrDefault()))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lComboBox.Name.Split('_').FirstOrDefault()).First();
                        if (lPropriedade.GetValue(pDM) != null && lPropriedade.GetValue(pDM).ToString() == lComboBox.Name.Split('_').LastOrDefault())
                            lComboBox.IsChecked = true;
                        else
                            lComboBox.IsChecked = false;
                    }
                }
            }

            return true;
        }

        public void GetControlsList(DependencyObject pControl, List<Object> pChildren, bool pNoCheck = false, bool pNoVisibilityCheck = false)
        {   
            int lChildNumber = VisualTreeHelper.GetChildrenCount(pControl);

            for (int i = 0; i <= lChildNumber - 1; i++)
            {
                var lChild = VisualTreeHelper.GetChild(pControl, i);

                if (lChild is TextBox && (pNoVisibilityCheck || ((TextBox)lChild).IsVisible))
                    pChildren.Add(lChild as TextBox);
                if (lChild is DatePicker && (pNoVisibilityCheck || ((DatePicker)lChild).IsVisible))
                    pChildren.Add(lChild as DatePicker);
                else if (lChild is ComboBox && (pNoVisibilityCheck || ((ComboBox)lChild).IsVisible))
                    pChildren.Add(lChild as ComboBox);
                else if (lChild is CheckBox && (pNoCheck || (bool)((CheckBox)lChild).IsChecked) && (pNoVisibilityCheck || ((CheckBox)lChild).IsVisible))
                    pChildren.Add(lChild as CheckBox);
                else if (lChild is RadioButton && (pNoCheck || (bool)((RadioButton)lChild).IsChecked) && (pNoVisibilityCheck || ((RadioButton)lChild).IsVisible))
                    pChildren.Add(lChild as RadioButton);

                if (VisualTreeHelper.GetChildrenCount(lChild) > 0)
                {
                    GetControlsList(lChild, pChildren, pNoCheck, pNoVisibilityCheck);
                }
            }
        }

        #endregion

        public class Item
        {
            public string Name { get; set; }
            public int? Value { get; set; }
            public decimal? Adicional { get; set; }
            public string Campo { get; set; }
            public Item(string name, int? value, decimal? adicional, string campo)
            {
                Name = name; Value = value; Adicional = adicional; Campo = campo;
            }
        }

        #region Validar CPF

        public static bool ValidateCPF(string pCPF)
        {
            string lCPFValue = pCPF.Replace(".", "");
            lCPFValue = lCPFValue.Replace("-", "");

            if (lCPFValue.Length != 11)
                return false;

            bool lContinue = true;
            for (int i = 1; i < 11 && lContinue; i++)
                if (lCPFValue[i] != lCPFValue[0])
                    lContinue = false;

            if (lContinue || lCPFValue == "12345678909")
                return false;

            int[] lNumbers = new int[11];
            for (int i = 0; i < 11; i++)
                lNumbers[i] = int.Parse(
                lCPFValue[i].ToString());

            int lSum = 0;
            for (int i = 0; i < 9; i++)
                lSum += (10 - i) * lNumbers[i];

            int lResult = lSum % 11;
            if (lResult == 1 || lResult == 0)
            {
                if (lNumbers[9] != 0)
                    return false;
            }
            else if (lNumbers[9] != 11 - lResult)
                return false;

            lSum = 0;
            for (int i = 0; i < 10; i++)
                lSum += (11 - i) * lNumbers[i];

            lResult = lSum % 11;

            if (lResult == 1 || lResult == 0)
            {
                if (lNumbers[10] != 0)
                    return false;

            }
            else
                if (lNumbers[10] != 11 - lResult)
                    return false;
            return true;

        }

        #endregion

        #region Validar CNPJ

        public static bool ValidateCNPJ(string pCNPJ)
        {

            string lCNPJValue = pCNPJ.Replace(".", "");
            lCNPJValue = pCNPJ.Replace("/", "");
            lCNPJValue = pCNPJ.Replace("-", "");

            int[] lDigits, lSum, lResult;
            int lDigitNumber;
            string ftmt;
            bool[] lIsTrue;

            ftmt = "6543298765432";
            lDigits = new int[14];
            lSum = new int[2];
            lSum[0] = 0;
            lSum[1] = 0;
            lResult = new int[2];
            lResult[0] = 0;
            lResult[1] = 0;
            lIsTrue = new bool[2];
            lIsTrue[0] = false;
            lIsTrue[1] = false;

            try
            {
                for (lDigitNumber = 0; lDigitNumber < 14; lDigitNumber++)
                {
                    lDigits[lDigitNumber] = int.Parse(
                     lCNPJValue.Substring(lDigitNumber, 1));
                    if (lDigitNumber <= 11)
                        lSum[0] += (lDigits[lDigitNumber] *
                        int.Parse(ftmt.Substring(
                          lDigitNumber + 1, 1)));
                    if (lDigitNumber <= 12)
                        lSum[1] += (lDigits[lDigitNumber] *
                        int.Parse(ftmt.Substring(
                          lDigitNumber, 1)));
                }

                for (lDigitNumber = 0; lDigitNumber < 2; lDigitNumber++)
                {
                    lResult[lDigitNumber] = (lSum[lDigitNumber] % 11);
                    if ((lResult[lDigitNumber] == 0) || (lResult[lDigitNumber] == 1))
                        lIsTrue[lDigitNumber] = (
                        lDigits[12 + lDigitNumber] == 0);

                    else
                        lIsTrue[lDigitNumber] = (
                        lDigits[12 + lDigitNumber] == (
                        11 - lResult[lDigitNumber]));

                }

                return (lIsTrue[0] && lIsTrue[1]);

            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region Validar CEP

        public static bool ValidateCEP(string pCEP)
        {
            if (pCEP.Length == 8)
            {
                pCEP = pCEP.Substring(0, 5) + "-" + pCEP.Substring(5, 3);
                //txt.Text = cep;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(pCEP, ("[0-9]{5}-[0-9]{3}"));
        }

        #endregion

        #region Validar E-mail

        public static bool ValidateEmail(string pEmail)
        {
            try
            {
                var lAddress = new System.Net.Mail.MailAddress(pEmail);
                return lAddress.Address == pEmail;
            }
            catch 
            {
                return false;
            }
            //return System.Text.RegularExpressions.Regex.IsMatch(pEmail, ("(?<user>[^@]+)@(?<host>.+)"));
        }

        #endregion

        #region Validar Placa

        public static bool ValidatePlate(string pPlate)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(pPlate, (@"^[a-zA-Z]{3}\-\d{4}$"));
        }

        #endregion
    } 
}
