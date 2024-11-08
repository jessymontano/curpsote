using Curp;
using Curp.Modelos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurpTest
{
    [TestFixture]
    public class CurpTests
    {
        private Mock<ICurp> _mockCurp;
        private List<Persona> _personas;

        [SetUp]
        public void SetUp()
        {
            _mockCurp = new Mock<ICurp>();
            _personas = new List<Persona>()
        {
            new Persona()
            {
                Curp = "MOLJ030113MSRNRSA0",
                Nombres = "Jessica",
                PrimerApellido = "Montaño",
                SegundoApellido = "Lares",
                Sexo = "Mujer",
                FechaNacimiento = DateOnly.Parse("2003-01-13"),
                Nacionalidad = "Mexico",
                EntidadNacimiento = "Sonora",
                TaBibito = true
            },
            new Persona()
            {
                Curp = "EUBP620630HMCSRB07",
                Nombres = "Pablo",
                PrimerApellido = "Esquivel",
                SegundoApellido = "Borja",
                Sexo = "Hombre",
                FechaNacimiento = DateOnly.Parse("1962-06-30"),
                Nacionalidad = "Mexico",
                EntidadNacimiento = "Mexico",
                TaBibito = false
            }
        };
            _mockCurp = new Mock<ICurp>();

            _mockCurp.Setup(x => x.curpExiste(It.IsAny<string>()))
                     .Returns((string curp) => _personas.Exists(p => p.Curp == curp));

            _mockCurp.Setup(x => x.obtenerPersonaConCurp(It.IsAny<string>()))
                     .Returns((string curp) =>
                     {
                         if (string.IsNullOrWhiteSpace(curp))
                             throw new ArgumentException("Te falta completar algún campo requerido. Por favor verifica.");

                         if (!System.Text.RegularExpressions.Regex.IsMatch(curp, "[A-Z]{4}\\d{6}[HMX]{1}[A-Z]{5}[\\dA-Z]{1}\\d"))
                             throw new ArgumentException("Algún campo es inválido. Por favor verifica.");

                         var persona = _personas.Find(p => p.Curp == curp);
                         if (persona == null)
                             throw new ArgumentException("No hay ninguna persona registrada con ese curp.");
                         if(persona!=null && !persona.TaBibito)
                         {
                             throw new ArgumentException("Esta CURP fue dada de baja por defunción.");
                         }

                         return persona;
                     });
        }
        [Test]
        public void ObtenerPersonaConCurp_IngresarCurpCorrecto_ObtenerPersona()
        {
            //Act
            var resultado = _mockCurp.Object.obtenerPersonaConCurp("MOLJ030113MSRNRSA0");

            //Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Jessica", resultado.Nombres);
        }

        [Test] 
        public void ObtenerPersonaConCurp_IngresarCurpDePersonaMuerta_LanzaExcepcion()
        {
            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _mockCurp.Object.obtenerPersonaConCurp("EUBP620630HMCSRB07"));
            Assert.AreEqual("Esta CURP fue dada de baja por defunción.", ex.Message);
        }

        [Test]
        public void ObtenerPersonaConCurp_IngresarCurpInexistente_LanzaExcepcion()
        {
            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _mockCurp.Object.obtenerPersonaConCurp("MOLJ990113MSRNRSA0"));
            Assert.AreEqual("No hay ninguna persona registrada con ese curp.", ex.Message);
        }

        [Test]
        public void ObtenerPersonaConCurp_CurpNulo_LanzaExcepcion()
        {
            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _mockCurp.Object.obtenerPersonaConCurp(""));
            Assert.AreEqual("Te falta completar algún campo requerido. Por favor verifica.", ex.Message);
        }

        [Test]
        public void ObtenerPersonaConCurp_FormatoIncorrecto_LanzaExcepcion()
        {
            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _mockCurp.Object.obtenerPersonaConCurp("dfgdfw"));
            Assert.AreEqual("Algún campo es inválido. Por favor verifica.", ex.Message);
        }
    }
   }

