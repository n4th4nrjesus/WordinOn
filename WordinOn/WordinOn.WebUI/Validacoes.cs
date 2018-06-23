using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordinOn.Models;

namespace WordinOn.WebUI
{
    public static class Validacoes
    {
        public static bool ValidarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;
            if (!email.Contains("@") || !email.Contains("."))
                return false;
            string[] strCamposEmail = email.Split(new String[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length != 2)
                return false;
            if (strCamposEmail[0].Length < 3)
                return false;
            if (!strCamposEmail[1].Contains("."))
                return false;
            strCamposEmail = strCamposEmail[1].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length < 2)
                return false;
            if (strCamposEmail[0].Length < 1)
                return false;
            return true;
        }

        public static bool ValidarCampos(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                return false;
            return true;
        }

        public static bool ValidarAvaliacao(Avaliacao obj)
        {
            if (String.IsNullOrWhiteSpace(obj.Texto))
                return false;
            var nota = Convert.ToString(obj.Valor);
            if (nota.Where(c => char.IsLetter(c)).Count() > 0)
                return false;
            if (obj.Valor > 1000)
                return false;
            return true;
        }

    }
}