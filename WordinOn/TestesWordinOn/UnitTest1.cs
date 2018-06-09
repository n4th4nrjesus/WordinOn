using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordinOn.Models;
using WordinOn.DataAccess;

namespace TestesWordinOn
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Redacao redacao = new Redacao();
			RedacaoDAO redacaoDAO = new RedacaoDAO();

			redacaoDAO.BuscarTodos();
		}
	}
}
