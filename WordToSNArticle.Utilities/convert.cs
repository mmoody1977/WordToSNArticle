using HtmlAgilityPack;
//using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WordToSNArticle.Domain;


namespace WordToSNArticle.Utilities
{
    public class convert : IDisposable
    {
       // private Application wordApp { get; set; }
        private dynamic wordApp { get; set; }
       // private Document wordDoc { get; set; }
        private dynamic wordDoc { get; set; }
        private HtmlDocument htmlDoc { get; set; }
        Type wordAppType { get; set; }
        Type wordDocType { get; set; }


        public convert()
        {
            wordAppType = Type.GetTypeFromProgID("Word.Application");
            wordDocType = Type.GetTypeFromProgID("Word.Document");
            //this.wordApp = new Application();
            this.wordApp = Activator.CreateInstance(wordAppType);
            this.htmlDoc = new HtmlDocument();
            this.wordDoc = Activator.CreateInstance(wordDocType);
        }

        public void InsertKBArticle(string[] args)
        {
            string docPath = args[0];
            string topic = args[1];
            string category = args[2];
            string password = "";
            // Instantiate the word document to convert            
            this.wordDoc = this.wordApp.Documents.Open(docPath, ReadOnly: true, Visible: false);

            #region Used for unlocking document. Disabled.
            // Unlock document if password protected.
            //if (this.wordDoc.ProtectionType.ToString() != "wdNoProtection" && args.Length < 4)
            //{
            //    Console.WriteLine("Error. Document is locked, but no password was provided.");
            //    return;
            //}
            //if (this.wordDoc.ProtectionType.ToString() != "wdNoProtection")
            //{
            //    password = args[3];
            //    this.wordDoc.Unprotect(password);
            //}
            #endregion

            string short_description = getFirstSentence();
           // this.Dispose();

            string htmlFilePath = saveDocumentAsHTML(ref docPath);
            this.Dispose();
            this.htmlDoc.Load(htmlFilePath);
            embedImages(htmlFilePath);

            Article newArticle = new Article { topic = topic, category = category, short_description = short_description, html = this.htmlDoc.DocumentNode.OuterHtml };
            this.htmlDoc.Save(htmlFilePath);

            insertInSN(newArticle);
            Console.WriteLine("");
            Console.WriteLine("SHORT DESCRIPTION: " + newArticle.short_description);
            Console.WriteLine("TOPIC: " + newArticle.topic);
            Console.WriteLine("CATEGORY: " + newArticle.category);
            Console.WriteLine("");
            Console.WriteLine("Press Enter key to close this console window");
            Console.ReadLine();
           // Console.WriteLine("HTML: " + newArticle.html);
           // Console.ReadLine();

        }

        private string getFirstSentence()
        {
            String read = string.Empty;
            List<string> data = new List<string>();
            for (int i = 0; i < this.wordDoc.Paragraphs.Count; i++)
            {
                string temp = this.wordDoc.Paragraphs[i + 1].Range.Text.Trim();
                if (temp != string.Empty)
                    data.Add(temp);
            }

            return data.FirstOrDefault();
        }

        private void insertInSN(Article newArticle)
        {

            // REST approach
            try
            {
                string baseURL = ConfigurationManager.AppSettings.Get("Instance_URL");
                string restURL = baseURL + "/kb_knowledge.do?JSONv2&sysparm_action=insert";
                HttpWebRequest req = WebRequest.Create(restURL) as HttpWebRequest;
                req.Credentials = new System.Net.NetworkCredential { UserName = ConfigurationManager.AppSettings.Get("username"), Password = ConfigurationManager.AppSettings.Get("password") };
                req.KeepAlive = false;
                req.Method = "POST";
                req.ContentType = "application/json"; //;charset=utf-8
                // Build JSON string
                string json = "{\"topic\":\"" + newArticle.topic + "\"," + "\"category\":\"" + newArticle.category + "\",\"short_description\":\"" + newArticle.short_description + "\",\"text\":\"" + newArticle.html.Replace("\"", "\\\"") + "\"}"; // + newArticle.html
               // Console.WriteLine("json: " + json);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                req.ContentLength = buffer.Length;
                Stream PostData = req.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                Encoding enc = System.Text.Encoding.GetEncoding(1252);
                StreamReader ResponseStream = new StreamReader(resp.GetResponseStream(), enc);

                string Response = ResponseStream.ReadToEnd();
                ResponseStream.Close();
                resp.Close();
                Console.Clear();
                
                string[] stringSeperator = new string[] { "," };
                string[] stringSeperators = new string[] { ":" };
                string[] responseArr = Response.Split(stringSeperator, StringSplitOptions.None);
                for (var i = 0; i < responseArr.Length; i++)
                {
                    string[] fieldArr = responseArr[i].Split(stringSeperators, StringSplitOptions.None);
                    if (fieldArr.Length == 2)
                    {
                        if (fieldArr[0].IndexOf("number") > -1)
                        {
                           // Console.WriteLine(fieldArr[1]);
                            Console.WriteLine("Article " + fieldArr[1] + " was successfully Inserted into the Knowledgebase on " + baseURL);
                          //  Console.ReadLine();
                        }
                    }
                }

              //  string[] responseArr1 = Response.Split(stringSeperators, StringSplitOptions.None);
                
               // Console.WriteLine(responseArr[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception encountered during Web Serice call: " + ex.Message);
            }


        }

        private void embedImages(string htmlFilePath)
        {
            // Fix Images so that they are embedded via base64
            var myNodes = this.htmlDoc.DocumentNode.Descendants("img");
            foreach (var subnode in myNodes)
            {
                /* <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==" alt="Red dot"> */
                // Console.WriteLine("IMG::: " + subnode.Name + " " + subnode.OuterHtml);
                // subnode.Attributes.Add("mike", "23");
                var srcAttribs = subnode.Attributes.AttributesWithName("src");
                if (srcAttribs.Count() > 1)
                {
                    Console.WriteLine("found more than one src attribute in one img tag");
                }
                else
                {
                    // Console.WriteLine(htmlFilePath);
                    string imagePath = "";
                    string[] filePathArr = htmlFilePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                    foreach (string part in filePathArr)
                    {
                        if (part != filePathArr[filePathArr.Count() - 1])
                        {
                            imagePath += part + "\\";
                        }
                    }
                    imagePath += srcAttribs.First().Value.ToString().Replace('/', '\\');
                    //Console.WriteLine("????: " + srcAttribs.First().Value.ToString());
                    //Console.WriteLine("Image Path: " + imagePath);
                    //Console.ReadLine();
                    string base64img = ConvertDocToBase64(imagePath);
                    string imageType = (srcAttribs.First().Value.Split(new char[] { '.' }))[1];
                    // Console.WriteLine("imageType: " + imageType);
                    srcAttribs.First().Remove();
                    subnode.Attributes.Add("src", "data:image/" + imageType + ";base64," + base64img);
                    // Console.WriteLine("Attribute: " + srcAttribs.First().Name + " value: " + srcAttribs.First().Value);
                }
            }
        }

        private string ConvertDocToBase64(string filePath)
        {
            // Console.WriteLine("Entered ConvertDocToBase64 function");
            byte[] binarydata = File.ReadAllBytes(filePath);
            // Console.WriteLine("Converted Doc to binarydata");
            string base64 = string.Empty;
            base64 = System.Convert.ToBase64String(binarydata, 0, binarydata.Length);
            return base64;
        }

        private string saveDocumentAsHTML(ref string document)
        {
            string htmlFilePath = "";
            object _nothing = System.Reflection.Missing.Value;
            object s_missing = System.Reflection.Missing.Value;
            object s_true = true;
            object s_false = false;
            object s_fileformat = 10; // WdSaveFormat.wdFormatFilteredHTML;
            object s_pdfFormat = 17; // WdSaveFormat.wdFormatPDF;
            string v = wordApp.Version;
            object compatMode = 12; // Microsoft.Office.Interop.Word.WdCompatibilityMode.wdWord2007;
            string _documentFile = document.Split(new string[] { "." }, StringSplitOptions.None)[0] + ".htm"; //"c:\\Convert\testMod.htm";
            //Console.WriteLine("File path: " + _documentFile);
            //Console.ReadLine();
            //object saveAs = "c:\\Convert\\testMod.pdf";
            string errorText = "";
            // Console.WriteLine("Path: " + _documentFile);
            Console.WriteLine("Office Version is " + v);
            Console.WriteLine("...");
            //  Console.ReadLine();
            try
            {
                switch (v)
                {
                    case "7.0":
                    case "8.0":
                    case "9.0":
                    case "10.0":
                        this.wordDoc.SaveAs2000(ref _documentFile, ref s_fileformat, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing);
                        break;
                    case "11.0":
                    case "12.0":
                        this.wordDoc.SaveAs(ref _documentFile, ref s_fileformat, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing, ref _nothing);
                        break;
                    case "14.0":
                        this.wordDoc.SaveAs2(ref _documentFile, ref s_fileformat);
                        //this.wordDoc.SaveAs2(ref _documentFile, ref s_fileformat, ref _nothing, ref _nothing, 
                        //    ref _nothing, ref _nothing, ref _nothing, ref _nothing, 
                        //    ref _nothing, ref _nothing, ref _nothing, ref _nothing, 
                        //    ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                        //            ref compatMode);
                        break;
                    case "15.0":
                        this.wordDoc.SaveAs2(_documentFile, s_fileformat);
                        #region Not used
                        // doc.SaveAs2(ref _documentFile, ref s_fileformat,
                        //    ref _nothing, ref _nothing, ref _nothing,
                        //    ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                        //    ref _nothing, ref _nothing, ref _nothing, ref _nothing,
                        //    ref _nothing, ref _nothing, ref _nothing,
                        //    ref _nothing);
                        // doc.SaveAs2("testHtmConversion3.htm", ref s_fileformat);
                        //this.wordDoc.SaveAs2(ref _documentFile, ref s_fileformat, ref s_missing, ref s_missing,
                        //    ref s_missing, ref s_missing, ref s_missing, ref s_missing,
                        //    ref s_missing, ref s_missing /*true */, ref s_missing, ref s_missing,
                        //    ref s_missing, ref s_missing, ref s_missing, ref s_missing, ref s_missing); // Convert word doc to html via interop SaveAs
                        #endregion
                        break;
                    default:
                        errorText = "Not able to get Word Version";
                        break;
                }
                Console.WriteLine("Document has been Saved!");
                Console.WriteLine("...");
                htmlFilePath = _documentFile.ToString();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error Saving file as html!: " + ex.Message);
                this.Dispose();
            }
            return htmlFilePath;
        }

        public void Dispose()
        {
            if (this.wordApp != null)
            {
                try
                {
                    object saveChanges = false;
                    object origFormat = this.wordDoc.SaveFormat;
                    object notTrue = false;
                    object missing = System.Reflection.Missing.Value;
                   // Console.WriteLine("Closing Word Doc");
                    this.wordDoc.Close(0);
                   // Console.WriteLine("Word Doc Closed");
                    this.wordDoc = null;
                    //this.wordApp.Application.Quit(ref notTrue, ref missing, ref missing); // Release the com object.
                   // Console.WriteLine("Closing Word App");
                    //this.wordApp.Quit(ref notTrue, ref missing, ref missing);
                    this.wordApp.Quit(saveChanges);
                   // Console.WriteLine("Word App Closed");
                    this.wordApp = null;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Error closing Word. Check Task manager and terminate process if left running: " + ex.Message);
                }
            }
        }
    }
}
