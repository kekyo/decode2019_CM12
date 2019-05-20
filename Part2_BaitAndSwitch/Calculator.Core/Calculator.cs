﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Host.Core;

namespace Calculator
{
    /// <summary>
    /// 計算処理の実体です。
    /// </summary>
    /// <remarks>このクラスは計算処理のみ行います。ホストの入出力はICalculatorHostインターフェイスを通じて間接的に実行します。</remarks>
    public static class CalculatorImplementation
    {
        /// <summary>
        /// 指定されたホストインターフェースを使用して、計算を実行します。
        /// </summary>
        /// <returns>タスク</returns>
        public static async Task CalculateAsync()
        {
            // 入出力を司るInteractionクラスを生成する
            using (var interaction = new Interaction())
            {
                await interaction.WriteLineAsync(
                    "Simple calculator - de:code 2019 CM12 Part2_BaitAndSwitch sample.");
                await interaction.WriteLineAsync(
                    "See https://github.com/kekyo/decode2019_CM12");
                await interaction.WriteLineAsync(
                    "Exit app    : exit");
                await interaction.WriteLineAsync(
                    "For example : 1 + 2 * 3");

                while (true)
                {
                    var line = await interaction.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    line = line.Trim();
                    if (line == "exit")
                    {
                        break;
                    }

                    var tokens = new Queue<string>(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                    try
                    {
                        var lhs = double.Parse(tokens.Dequeue());

                        while (tokens.Count >= 1)
                        {
                            var oper = tokens.Dequeue();
                            var rhs = double.Parse(tokens.Dequeue());

                            switch (oper)
                            {
                                case "+":
                                    lhs += rhs;
                                    break;
                                case "-":
                                    lhs -= rhs;
                                    break;
                                case "*":
                                    lhs *= rhs;
                                    break;
                                case "/":
                                    lhs /= rhs;
                                    break;
                                case "%":
                                    lhs %= rhs;
                                    break;
                                default:
                                    throw new ArgumentException("Unknown operator", oper);
                            }
                        }

                        await interaction.WriteLineAsync(
                            "Result={0}",
                            lhs);
                    }
                    catch (Exception ex)
                    {
                        await interaction.WriteLineAsync(
                            "Error: {0}: {1}",
                            Marshal.GetHRForException(ex),
                            ex.Message);
                    }
                }
            }
        }
    }
}
