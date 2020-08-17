namespace PR.Services.ModelResponse
{
    /// <summary>
    /// Objeto modelo para la devolucion de valores tipo diccionario para cargar combos
    /// </summary>
    public class ComboItem
    {
        /// <summary>
        /// Texto
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Valor
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Segundo Valor
        /// </summary>
        public string SecodValue { get; set; }
    }
}