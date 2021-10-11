using Microsoft.Office.Interop.Word;
using System;
using System.Collections;

namespace ProjectEcho
{
    internal class FormatChecker
    {
		public static Application ap = new Application();
		public Boolean[] runFormatCheck(String path, int correctLength)
		{
			//path = @"C:\Users\ceseg\Desktop\AlterEgo.docx";
            Document document = ap.Documents.Open(FileName: path, Visible: false, ReadOnly: false);

			/**
			 *                                 ConfirmConversions: false,
                                               ReadOnly: false,
                                               AddToRecentFiles: true,
                                               PasswordDocument: null,
                                               PasswordTemplate: null,
                                               Revert: null,
                                               WritePasswordDocument: null,
                                               WritePasswordTemplate: null,
                                               Format: null,
                                               Encoding: 20127,
                                               Visible: false,
                                               OpenAndRepair: false,
                                               DocumentDirection: 0,
                                               NoEncodingDialog: false,
                                               XMLTransform: null
			 * 
			 * 
			 */

			Boolean isAligned = checkAlignment(document);
			Boolean isArial = checkFont(document);
			Boolean isFontSize = checkFontSize(document);
			Boolean isCorrectLength = false;

			int actualLength = checkLength(document);
			if(correctLength == actualLength || actualLength < correctLength)
            {
				isCorrectLength = true;
            }
			Boolean[] isFormatted = {isAligned, isArial, isFontSize, isCorrectLength};

			document.Close();
			//ap.Quit();
			return isFormatted;
		}

		public Boolean checkAlignment(Document document)
		{

			return false;
		}

		public Boolean checkFontSize(Document document)
		{
			if(document.Content.Font.Size != 12)
            {
				System.Windows.Forms.MessageBox.Show(document.Content.Font.Size.ToString());
				return false;
			}
			return true;
		}

		public Boolean checkFont(Document document)
		{
			Boolean isCorrectFont = true;
			Font correct = new Font();
			correct.Name = "Times New Roman";
			Font blank = new Font();
			foreach(Microsoft.Office.Interop.Word.Paragraph para in document.Paragraphs)
			{
				//System.Windows.Forms.MessageBox.Show(para.)
				if(para.Range.Font.Name.CompareTo(correct.Name) != 0)
				{
                    if(para.Range.Font.Name.CompareTo(blank.Name) != 0)
                    {
						System.Windows.Forms.MessageBox.Show(para.Range.Font.Name);
						isCorrectFont = false;
						break;
					} 
				}
			}
			return isCorrectFont;
		}

		public int checkLength(Document document)
		{
			System.Windows.Forms.MessageBox.Show(document.Content.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdNumberOfPagesInDocument).ToString());
			return document.Content.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdNumberOfPagesInDocument);
		}
	}
}
