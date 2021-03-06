﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace iban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onBtnCalcClick(object sender, RoutedEventArgs e)
        {
            string blz = TEXTBOX_BLZ.Text;
            string kto = TEXTBOX_KTO.Text;
            bool digitsOnly = blz.Length > 0 && kto.Length > 0 && blz.All(char.IsDigit) && kto.All(char.IsDigit);
            if ( digitsOnly )
            {
                string ktoAsString10 = string.Format("{0:0000000000}", Convert.ToDecimal(TEXTBOX_KTO.Text)); // limit to 10 chars (leading zeros, if account number shorter than 10 chars)
                string bban = blz + ktoAsString10 + "131400";
                decimal bbanAsDec = Convert.ToDecimal(bban);
                decimal chksum = bbanAsDec % 97;
                chksum = 98 - chksum;
                string chksumAsString = string.Format("{0:00}", chksum); // fixed to 2 chars (one leading zero, if checksum has only 1 char)
                string iban = "DE" + chksumAsString + blz + ktoAsString10;
                //TEXTBOX_IBAN.Text = iban; // Only this line required for textbox, but without color

                // Create a FlowDocument
                FlowDocument mcFlowDoc = new FlowDocument();

                // Create a paragraph with text
                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run("DE") { Background = Brushes.LightGray });
                para.Inlines.Add(new Run(chksumAsString) { Background = Brushes.LightSkyBlue });
                para.Inlines.Add(new Run(blz) { Background = Brushes.Yellow });
                para.Inlines.Add(new Run(ktoAsString10) { Background = Brushes.LightGreen });

                // Add the paragraph to blocks of paragraph
                mcFlowDoc.Blocks.Add(para);

                // Set contents
                RICHTEXTBOX_IBAN.Document = mcFlowDoc;
            }
            else
            {
                // Create a FlowDocument
                FlowDocument mcFlowDoc = new FlowDocument();

                // Create a paragraph with text
                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run("Nur Zahlen und keine Leerzeichen"));

                // Add the paragraph to blocks of paragraph
                mcFlowDoc.Blocks.Add(para);

                // Set contents
                RICHTEXTBOX_IBAN.Document = mcFlowDoc;
                //TEXTBOX_IBAN.Text = "Nur Zahlen und keine Leerzeichen"; // Only this line required for textbox (but then without color)
            }
        }
    }
}
