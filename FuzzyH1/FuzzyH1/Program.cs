using System;

public class FuzzyLogicModificada
{
    // Funções de pertinência para os termos fuzzy 'baixo', 'medio', 'alto'
    public static double PertinenciaBaixo(double entrada)
    {
        if (entrada <= 0) return 1;
        if (entrada > 0 && entrada <= 30) return 1 - (entrada / 30); // Intervalo ajustado para 0-30
        return 0;
    }

    public static double PertinenciaMedio(double entrada)
    {
        if (entrada > 0 && entrada <= 30) return 0;  // Fora da faixa do 'medio'
        if (entrada > 30 && entrada <= 60) return (entrada - 30) / 30;  // Transição suave para 'medio'
        if (entrada >= 60 && entrada <= 90) return 1 - Math.Abs(entrada - 75) / 15;  // 'medio' com máximo deslocado para 75
        if (entrada > 90 && entrada <= 120) return (entrada - 90) / 30;  // Transição para 'alto'
        return 0;
    }

    public static double PertinenciaAlto(double entrada)
    {
        if (entrada > 60 && entrada <= 120) return (entrada - 60) / 60; // Intervalo ajustado para 60-120
        return 0;
    }

    // Fuzzificação: Mapeia o valor crisp para os termos fuzzy
    public static (double baixo, double medio, double alto) FuzzificarValor(double entrada)
    {
        double baixo = PertinenciaBaixo(entrada);
        double medio = PertinenciaMedio(entrada);
        double alto = PertinenciaAlto(entrada);

        return (baixo, medio, alto);
    }

    // Função de inferência fuzzy (usando uma regra modificada)
    public static double CalcularInferencia(double baixo, double medio, double alto)
    {
        // Alterando os pesos das regras de inferência
        double resultadoFuzzy = (medio * 1.2) + (baixo * 0.6) + (alto * 0.8); // Pesos ajustados
        return resultadoFuzzy / (baixo + medio + alto); // Média ponderada dos graus de pertinência
    }

    // Defuzzificação (Método do centro de gravidade)
    public static double ConverterFuzzyParaCrisp(double resultadoFuzzy)
    {
        // Convertendo o valor fuzzy de volta para crisp
        double valorFinal = resultadoFuzzy * 120; // Ajuste de escala para 0-120
        return valorFinal;
    }

    public static void Main(string[] args)
    {
        // Exemplo de valor crisp
        double entradaUsuario = 45; // Alterado para um valor no intervalo modificado

        // Fuzzificação
        var (baixo, medio, alto) = FuzzificarValor(entradaUsuario);

        // Exibindo os valores fuzzificados
        Console.WriteLine($"Valor crisp {entradaUsuario} mapeado para o termo fuzzy 'baixo' com grau de pertinência {baixo}");
        Console.WriteLine($"Valor crisp {entradaUsuario} mapeado para o termo fuzzy 'medio' com grau de pertinência {medio}");
        Console.WriteLine($"Valor crisp {entradaUsuario} mapeado para o termo fuzzy 'alto' com grau de pertinência {alto}");

        // Inferência fuzzy
        double resultadoFuzzy = CalcularInferencia(baixo, medio, alto);
        Console.WriteLine($"Resultado da inferência fuzzy: {resultadoFuzzy}");

        // Defuzzificação
        double valorCrispFinal = ConverterFuzzyParaCrisp(resultadoFuzzy);
        Console.WriteLine($"Valor crisp após defuzzificação: {valorCrispFinal}");

        // Resultado final
        Console.WriteLine($"Resultado crisp final: {valorCrispFinal}");
    }
}
