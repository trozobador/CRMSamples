        /// <summary>
        /// Obtém valor de variável de ambiente pelo "schemaname"
        /// </summary>
        /// <param name="nomeVariavel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string ObterVariavelAmbiente(string nomeVariavel)
        {
            var query = new QueryExpression("environmentvariabledefinition");
            query.TopCount = 1;
            query.ColumnSet.AddColumns("defaultvalue");
            query.Criteria.AddCondition("schemaname", ConditionOperator.Equal, nomeVariavel);

            var evv = query.AddLink("environmentvariablevalue", "environmentvariabledefinitionid", "environmentvariabledefinitionid", JoinOperator.LeftOuter);
            evv.EntityAlias = "evv";
            evv.Columns.AddColumns("value");

            var retrieveMultiple = Provider.RetrieveMultiple(query);
            if (retrieveMultiple?.Entities.Count > 0)
            {
                var entity = retrieveMultiple.Entities?.FirstOrDefault();
                var valorNovo = entity.GetAttributeValue<AliasedValue>("evv.value")?.Value?.ToString();
                var valorDefault = entity.GetAttributeValue<string>("defaultvalue");
                var valor = valorNovo ?? valorDefault;
                if (valor != null)
                    return valor;
                else
                    throw new Exception($"Variável de ambiente: {nomeVariavel} não existe valor.");
            }
            else
                throw new Exception($"Variável de ambiente: {nomeVariavel} não existe no ambiente.");
        }
