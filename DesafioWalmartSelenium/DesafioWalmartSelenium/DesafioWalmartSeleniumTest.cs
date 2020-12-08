using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesafioWalmartSelenium
{
    [TestClass]
    public class DesafioWalmartSeleniumTest
    {
        private static IWebDriver wDriver;

        [ClassInitialize]
        public static void Inicializar(TestContext testContext)
        {
            wDriver = new FirefoxDriver();
            wDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5f);
            wDriver.Manage().Window.Maximize();

        }

        [ClassCleanup]
        public static void Finalizar()
        {
            wDriver.Quit();
        }

        [TestMethod]
        public void BuscarProductoSinTexto()
        {
           
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));


            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            string attrDisabled = btnBuscar.GetAttribute("disabled");


            Assert.IsTrue(attrDisabled == "true");
        }


        [TestMethod]
        public void BotonSearchSegunLargoTexto()
        {
           
            wDriver.Navigate().GoToUrl("http://localhost:55671/");

            //el boton debe partir desactivado, luego pasar a activo y luego desactivarse nuevamente
            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));

            string attrDisabledBtnIni = btnBuscar.GetAttribute("disabled");

            inputSearch.SendKeys("test");            
            string attrDisabledBtnConTexto = btnBuscar.GetAttribute("disabled");

            inputSearch.Clear();
            inputSearch.SendKeys("tes");

            string attrDisabledBtnConOtroTexto = btnBuscar.GetAttribute("disabled");
            
            
            Assert.IsTrue(attrDisabledBtnIni == "true" && attrDisabledBtnConTexto == null && attrDisabledBtnConOtroTexto == "true");
        }

        [TestMethod]
        public void BuscarProductoPorIdPalindromo()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");
            

            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("1331");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();

            

            IReadOnlyCollection<IWebElement> listaDivsProductos = wDriver.FindElements(By.ClassName("producto"));
            IReadOnlyCollection<IWebElement> listaPreciosDescuentos = wDriver.FindElements(By.ClassName("precioDescuento"));

            Assert.IsTrue(listaDivsProductos.Count == 1 && listaPreciosDescuentos.Count == 1);
        }

        [TestMethod]
        public void BuscarProductoPorIdNoPalindromo()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("1555");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();

            IReadOnlyCollection<IWebElement> listaDivsProductos = wDriver.FindElements(By.ClassName("producto"));
            IReadOnlyCollection<IWebElement> listaPreciosDescuentos = wDriver.FindElements(By.ClassName("precioDescuento"));

            Assert.IsTrue(listaDivsProductos.Count == 1 && listaPreciosDescuentos.Count == 0);
        }


        [TestMethod]
        public void BuscarProductoPorTextoPalindromo()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("dsaasd");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();


            IReadOnlyCollection<IWebElement> listaDivsProductos = wDriver.FindElements(By.ClassName("producto"));
            IReadOnlyCollection<IWebElement> listaPreciosDescuentos = wDriver.FindElements(By.ClassName("precioDescuento"));

            Assert.IsTrue(listaDivsProductos.Count > 0 && listaPreciosDescuentos.Count > 0);
        }

        [TestMethod]
        public void BuscarProductoPorIdMenosDeTresDigitos()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("7");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();

            IReadOnlyCollection<IWebElement> listaDivsProductos = wDriver.FindElements(By.ClassName("producto"));
            

            Assert.IsTrue(listaDivsProductos.Count == 1);
        }


        [TestMethod]
        public void BuscarProductoPorTextoCuatroCaracteresExiste()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("nvnactr");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();

            IReadOnlyCollection<IWebElement> listaDivsProductos = wDriver.FindElements(By.ClassName("producto"));


            Assert.IsTrue(listaDivsProductos.Count > 0);
        }


        [TestMethod]
        public void BuscarProductoPorTextoMuchosCaracteresNoExiste()
        {
            
            wDriver.Navigate().GoToUrl("http://localhost:55671/");


            IWebElement inputSearch = wDriver.FindElement(By.Id("txtBuscador"));
            inputSearch.SendKeys("nbncvqeqvn424act5345r sdfsg3423");

            IWebElement btnBuscar = wDriver.FindElement(By.Id("btnBuscar"));
            btnBuscar.Click();

            IWebElement divSinDatos = wDriver.FindElement(By.ClassName("sinDatos"));


            Assert.IsTrue(divSinDatos != null);
        }
    }
}
