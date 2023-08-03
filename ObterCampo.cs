        /// <summary>
        /// Retorna o valor do campo informado 
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="target"></param>
        /// <param name="preImage"></param>
        /// <returns></returns>
        public static T ObterCampo<T>(string nomeCampo, Entity target, Entity preImage)
        {
                try
                {
                    return target.GetAttributeValue<T>(nomeCampo);
                }
                catch
                {
                    return default;
                }
            return default;
        }

        public static T ObterCampo<T>(string nomeCampo, Entity target, bool isAlias = false)
        {
            if (target == null || string.IsNullOrEmpty(nomeCampo))
                return default;

            if (target != null && target.Attributes.ContainsKey(nomeCampo))
            {
                try
                {
                    if (isAlias)
                        return ((T)target.GetAttributeValue<AliasedValue>(nomeCampo).Value);
                    return target.GetAttributeValue<T>(nomeCampo);
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }
