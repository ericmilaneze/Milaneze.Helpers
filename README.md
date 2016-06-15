# Milaneze.Helpers
Classes com utilitários usados geralmente por todo desenvolvedor: tratamento de string; operações com datas; tratamento e validação de CNPJ e CPF; números escritos por extenso; entre outras funcionalidades.

Escrito em C# com o Visual Studio 2015 Community.

NuGet: https://www.nuget.org/packages/Milaneze.Helpers/

PM> Install-Package Milaneze.Helpers

## Exemplo
using Milaneze.Helpers; // ativa os Extension Methods
...
string n1 = "eric    milanéze   ";
string n2 = nome.RemoverEspacosDuplicados().RemoverAcentos().PrimeirasLetrasPalavraMaiusculas(); // Eric Milaneze


## Funcionalidades de String (Extension Methods)
* Método SafeSubstring(): não gera Exceptions como o Substring().
* Remoção de acentos.
* ReplaceFirst(): substitui a primeira ocorrência de uma string.
* Remoção de espaços duplicados.
* Remoção de caracteres não numéricos.
* Remoção de caracteres especiais para formar nome de arquivo.
* Captura de números em strings.
* Escrita de números por extenso (exemplo: "100" >> "cem").
* Validação de strings com caracteres iguais.
* Verificação se string é um número.
* Transformação de primeiro caractere de string em maiúsculo.
* Transformação de primeira letra de cada palavra em maiúsculo (para nomes próprios, por exemplo).

## Funcionalidades de DateTime (Extension Methods)
* Escrita de data por extenso.
* Cálculo de idade.
* Cálculo de tempo de trabalho, considerando início de fim de jornada, pausas, feriados, entre outras possibilidades customizáveis.

## Funcionalidades de Char (Extension Methods)
* Verificação de caractere numérico.

## Funcionalidades de Array (Extension Methods)
* Verificação de existência de posição de um ICollection.

## CPF
* Validação.
* Formatação.

## CNPJ
* Validação.
* Formatação

## Funcionalidades Utilitárias
* Lista de feriados do ano (feriados fixos e móveis, como carnaval).