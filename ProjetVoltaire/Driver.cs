﻿using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V120.Network;
using OpenQA.Selenium.Interactions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProjetVoltaire
{
    class Driver
    {
        private readonly ChromeDriver driver;
        private readonly IDevToolsSession session;
        private readonly OpenQA.Selenium.DevTools.V120.DevToolsSessionDomains domains;
        // Define the event
        public event EventHandler<string> DataFound;

        public Driver(string path)
        {
            // Set up ChromeDriverService with a random port
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(path);
            service.Port = 0;
            // Setting up the webdriver
            Console.WriteLine("DriverInfo -> Starting Webdriver");
            driver = GetChromeDriver(path);
            Console.WriteLine("DriverInfo -> Managing timeouts");

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            Console.WriteLine("DriverInfo -> Creating Devtools");
            var devTools = (IDevTools)driver;
            session = devTools.GetDevToolsSession();
            Console.WriteLine("DriverInfo -> Session Openned");

            // Enable the Network domain
            domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V120.DevToolsSessionDomains>();
            domains.Network.Enable(new OpenQA.Selenium.DevTools.V120.Network.EnableCommandSettings());
            domains.Network.ResponseReceived += ResponseReceivedHandler;

            Console.WriteLine("DriverInfo -> Navigating");
            string baseUrl = "https://www.projet-voltaire.fr/";
            driver.Navigate().GoToUrl(baseUrl);
            Console.WriteLine("DriverInfo -> All Done !");
        }
        static ChromeDriver GetChromeDriver(string path)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddExcludedArgument("enable-logging");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument($"user-data-dir={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile")}");

            return new ChromeDriver(path, chromeOptions);
        }
        public bool IsExercice()
        {
            try
            {
                // Find the elements using the specified locator
                var elements = driver.FindElements(By.XPath($"//div[@class='sentenceClickView']"));

                // Check if the list of elements is not empty
                return elements.Any();
            }
            catch (NoSuchElementException)
            {
                // Element not found
                return false;
            }
        }
        public string FindString()
        {
            if (GetSentenceDiv(out var sentenceNode, out var sentenceElement))
            {
                string sentence = "";
                foreach (var word in sentenceNode.SelectNodes(".//span[@class='pointAndClickSpan']"))
                {
                    sentence += word.InnerText;
                }
                return sentence.Replace("&nbsp;", " ").Replace("‑", "-");
            }
            else
            {
                return "None";
            }
        }
        public void ClickChar(int indx)
        {
            int i = 0;
            int chars = 0;
            if (GetSentenceDiv(out var sentenceNode, out var sentenceElement))
            {
                HtmlNodeCollection nodeCollection = sentenceNode.SelectNodes(".//span[@class='pointAndClickSpan']");
                var elementCollection = sentenceElement.FindElements(By.XPath(".//span[@class='pointAndClickSpan']"));
                while (i < nodeCollection.Count) 
                { 
                    chars += nodeCollection[i].InnerText.Length;
                    if (chars > indx)
                    {
                        IWebElement wordElement = elementCollection[i];
                        Console.WriteLine("DriverInfo -> clicking \"" + nodeCollection[i].InnerText + "\"");
                        wordElement.Click();
                        return;
                    }
                    i++;
                }
            }
        }
        public void ClickNoMistakes()
        {
            try
            {
                IWebElement button = driver.FindElement(By.Id("btn_question_suivante"));
                button.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DriverError -> {ex}");
            }
            //IWebElement button = driver.FindElement(By.XPath("//div[@class='noMistakeButton']"));
            //button.Click();
        }
        public void ClickNext()
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
        }
        public bool HasExercice()
        {
            //class intensiveTraining
            IWebElement parent = driver.FindElement(By.XPath("//div[@class='sheetView']"));
            return(IsElementPresentByClassName(parent, "intensiveTraining"));
        }
        public void ClickSkipExercice()
        {
            //class understoodButton
            //class buttonKo
            //class exitButton secondaryButton
            Thread.Sleep(3000);

            IWebElement parent = driver.FindElement(By.XPath("//div[@class='intensiveTraining']"));
            try
            {
                IWebElement button = parent.FindElement(By.ClassName("understoodButton"));
                button.Click();

                IWebElement[] buttons = parent.FindElements(By.ClassName("buttonKo")).ToArray();
                foreach (IWebElement bu in buttons)
                        bu.Click();

            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("DriverError -> Element not found");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"DriverError -> {ex}");
            }
            bool foundSbutton = false;
            bool foundPbutton = false;
            IWebElement sbutton = null;
            IWebElement pbutton = null;
            try
            {
                sbutton = parent.FindElement(By.CssSelector(".exitButton.secondaryButton"));
                foundSbutton = true;
            }
            catch (NoSuchElementException)
            {
                foundSbutton = false;
            }
            try
            {
                pbutton = parent.FindElement(By.CssSelector(".exitButton.primaryButton"));
                foundPbutton = true;
            }
            catch (NoSuchElementException)
            {
                foundPbutton = false;
            }
            if (foundSbutton)
                sbutton?.Click();
            else if (foundPbutton)
                pbutton?.Click();
            else
                Console.WriteLine("DriverError -> The exit button was not found.");
        }
        //0 - good
        //1 - bad
        //2 - idk
        public int AwnserState()
        {
            IWebElement parent = driver.FindElement(By.XPath("//div[@class='sheetAnswerStatusBarContainer']"));
            var incorrect          = IsElementPresentByClassName(parent, "answerStatusBar incorrect mistakePresent")          || IsElementPresentByClassName(parent, "answerStatusBar incorrect noMistake");
            var critical_incorrect = IsElementPresentByClassName(parent, "answerStatusBar incorrect critical mistakePresent") || IsElementPresentByClassName(parent, "answerStatusBar incorrect critical noMistake") ;
            var correct            = IsElementPresentByClassName(parent, "answerStatusBar correct mistakePresent")            || IsElementPresentByClassName(parent, "answerStatusBar correct noMistake");
            var critical_correct   = IsElementPresentByClassName(parent, "answerStatusBar correct critical mistakePresent")   || IsElementPresentByClassName(parent, "answerStatusBar correct critical noMistake");
            if (correct || critical_correct )
            {
                return 0;
            }
            else if (incorrect || critical_incorrect)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }
        static bool IsElementPresentByClassName(IWebElement parent, string elementClassName)
        {
            string divXPath = $"//div[@class='{elementClassName}']";

            try
            {
                // Find the elements using the specified locator
                var elements = parent.FindElements(By.XPath(divXPath));

                // Check if the list of elements is not empty
                return elements.Any();
            }
            catch (NoSuchElementException)
            {
                // Element not found
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DriverError -> {ex}");
                return false;
            }
        }
        private bool GetSentenceDiv(out HtmlNode sentenceNode, out IWebElement sentenceElement)
        {
            // Retrieving the sentence to check
            string source = driver.PageSource;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);

            sentenceElement = driver.FindElement(By.XPath("//div[@class='sentence']"));
            sentenceNode = doc.DocumentNode.SelectSingleNode("//div[@class='sentence']");
            return (sentenceElement != null && sentenceNode != null);
        }
        async void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e)
        {
            string targetUrl = "https://www.projet-voltaire.fr/services-pjv/gwt/WolLearningContentWebService";

            if (e.Response.MimeType == "application/json" && e.Response.Url == targetUrl)
            {
                Console.WriteLine("DriverInfo -> Found a response");

                // Try to get the response body with retries
                var responseBody = await GetResponseBodyWithRetries(e.RequestId);

                if (responseBody != null)
                {
                    Console.WriteLine("DriverInfo -> Length: " + responseBody.Body.Length);
                    if (responseBody.Body.Length > 10000)
                    {
                        DataFound?.Invoke(this, responseBody.Body);
                    }
                    else
                    {
                        Console.WriteLine("DriverInfo -> Response not long enough");
                    }
                }
                else
                {
                    Console.WriteLine("DriverWarning -> Failed to retrieve response body.");
                }
            }
        }
        async Task<GetResponseBodyCommandResponse> GetResponseBodyWithRetries(string requestId, int maxRetries = 3)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var cmd = new GetResponseBodyCommandSettings { RequestId = requestId };
                    var data = await domains.Network.GetResponseBody(cmd);
                    return data;
                }
                catch (CommandResponseException ex)
                {
                    // Log or handle the exception if needed
                    Console.WriteLine($"Driver -> Attempt {attempt}: {ex.Message}");
                }

                // Wait for a short duration before retrying
                await Task.Delay(500);
            }

            return null; // Return null if all retries fail
        }
        public void Quit()
        {
            driver.Quit();
        }
    }
}
