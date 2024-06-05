using System;
using System.Threading;
using MySql.Data.MySqlClient;

namespace MyApp
{
    internal class Program
    {
        static string connectionString = "server=localhost;user=root;database=bd_padilha;port=3306;";
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Bem Vindo Ao Sistema de HelpDesk");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("                                  ");
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Cadastrar");


            Console.WriteLine("Digite sua escolha: ");
            int escolha1 = Convert.ToInt32(Console.ReadLine());

            switch (escolha1)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Cadastro();
                    break;

            }
        }

        static void Cadastro()
        {
            Console.Clear();
            Console.WriteLine("Informe os seus dados para realizar o cadastro");
            Console.WriteLine("-----------------------------------------------");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Sobrenome: ");
            string sobrenome = Console.ReadLine();
            Console.Write("DDD: ");
            int ddd = Convert.ToInt32(Console.ReadLine());
            Console.Write("Telefone: ");
            int telefone = Convert.ToInt32(Console.ReadLine());
            Console.Write("CPF: ");
            double cpf = Convert.ToDouble(Console.ReadLine());
            Console.Write("Data de Nascimento(DD-MM-AAAA): ");
            string datadenascimento = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = ReadPassword();

            Inserir_dados(nome, sobrenome, ddd, telefone, cpf, datadenascimento, email, senha);

            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Cadastro realizado com sucesso...");
            Thread.Sleep(2000);
            Console.Clear();
            Menu();
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("Bem-Vindo (a)");
            Console.WriteLine("-------------------------------------------");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = ReadPassword();

            Thread.Sleep(2000);

            Console.Clear();

            Console.WriteLine("FAÇA SUA ESCOLHA: ");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("[1] Abrir Chamada ");
            Console.WriteLine("[2] Acompanhar Status da Chamada ");
            Console.WriteLine("[3] Atualizar Chamada ");
            Console.WriteLine("[4] Excluir Chamada");

            Console.WriteLine("Digite sua escolha: ");
            int escolha2 = Convert.ToInt32(Console.ReadLine());

            switch (escolha2)
            {
                case 1:
                    Gera_Ticket();
                    break;

                case 2:
                    AcompanharStatusChamada();
                    break;

                case 3:
                    AtualizarChamada();
                    break;

                case 4:
                    ExcluirChamada();
                    break;
            }


        }

        static void Gera_Ticket()
        {
            Console.Clear();
            Console.WriteLine("Registre sua chamada ");
            Console.WriteLine("----------------------");

            Random random = new Random();
            int ticketNumber = random.Next(100000, 999999);

            Console.Write("Digite a prioridade (baixa, média, alta, urgente): ");
            string prioridade = Console.ReadLine();
            Console.Write("Digite a descrição (Software, Hardware): ");
            string descricao = Console.ReadLine();
            Console.Write("Digite a observação (descreva o problema): ");
            string observacao = Console.ReadLine();
            Console.Write("Digite a data(DD-MM-AAAA): ");
            string data_chamada = Console.ReadLine();

            Console.Clear();
            Console.Write("Solicitação gerada com sucesso...");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Número do Ticket: " + ticketNumber);
            Console.WriteLine("===========================================");
            Console.WriteLine("Prioridade: " + prioridade);
            Console.WriteLine("===========================================");
            Console.WriteLine("Descrição: " + descricao);
            Console.WriteLine("===========================================");
            Console.WriteLine("Observação: " + observacao);
            Console.WriteLine("Pressione ENTER para finalizar... ");
            Console.ReadKey();
            Thread.Sleep(2000);

            Inserir_ticket(prioridade, descricao, observacao, ticketNumber, data_chamada);
            Menu();
        }

        static void Inserir_dados(string nome, string sobrenome, int ddd, int telefone, double cpf, string datadenascimento, string email, string senha)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO usuarios (nome, sobrenome, ddd, telefone, cpf, datadenascimento, email, senha) VALUES (@nome, @sobrenome, @ddd, @telefone, @cpf, @datadenascimento, @email, @senha)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@sobrenome", sobrenome);
                        cmd.Parameters.AddWithValue("@ddd", ddd);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@cpf", cpf);
                        cmd.Parameters.AddWithValue("@datadenascimento", datadenascimento);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@senha", senha);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.Clear();
                Console.WriteLine("Carregando.....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar: " + ex.Message);
            }
        }

        static void Inserir_ticket(string prioridade, string descricao, string observacao, int ticketNumber, string data_chamada)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO registro_de_chamada (prioridade, descricao, observacao, ticket, data_chamada, status) VALUES (@prioridade, @descricao, @observacao, @ticket, @data_chamada, @status)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@prioridade", prioridade);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.Parameters.AddWithValue("@observacao", observacao);
                        cmd.Parameters.AddWithValue("@ticket", ticketNumber);
                        cmd.Parameters.AddWithValue("@data_chamada", data_chamada);
                        cmd.Parameters.AddWithValue("@status", "Aberto");  // Status inicial
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine(" ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao registrar problema: " + ex.Message);
            }
        }

        static void AcompanharStatusChamada()
        {
            Console.Clear();
            Console.WriteLine("Acompanhar Status da Chamada");
            Console.WriteLine("-----------------------------");

            Console.Write("Digite o número do ticket: ");
            int ticketNumber = Convert.ToInt32(Console.ReadLine());

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM registro_de_chamada WHERE ticket = @ticket";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ticket", ticketNumber);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("Ticket: " + reader["ticket"]);
                                Console.WriteLine("Prioridade: " + reader["prioridade"]);
                                Console.WriteLine("Descrição: " + reader["descricao"]);
                                Console.WriteLine("Observação: " + reader["observacao"]);
                                Console.WriteLine("Data da Chamada: " + reader["data_chamada"]);
                                Console.WriteLine("Status: " + reader["status"]);
                            }
                            else
                            {
                                Console.WriteLine("Ticket não encontrado!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar status da chamada: " + ex.Message);
            }

            Console.WriteLine("Pressione ENTER para retornar ao menu...");
            Console.ReadKey();
            Menu();
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        static void AtualizarChamada()
        {
            Console.Clear();
            Console.WriteLine("Atualizar Chamada");
            Console.WriteLine("-------------------");

            Console.Write("Digite o número do ticket: ");
            int ticketNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nova prioridade (baixa, média, alta, urgente): ");
            string novaPrioridade = Console.ReadLine();

            Console.Write("Nova descrição (Software, Hardware): ");
            string novaDescricao = Console.ReadLine();

            Console.Write("Nova observação (descreva o problema): ");
            string novaObservacao = Console.ReadLine();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE registro_de_chamada SET prioridade = @novaPrioridade, descricao = @novaDescricao, observacao = @novaObservacao WHERE ticket = @ticket";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@novaPrioridade", novaPrioridade);
                        cmd.Parameters.AddWithValue("@novaDescricao", novaDescricao);
                        cmd.Parameters.AddWithValue("@novaObservacao", novaObservacao);
                        cmd.Parameters.AddWithValue("@ticket", ticketNumber);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Chamada atualizada com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Ticket não encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar chamada: " + ex.Message);
            }

            Console.WriteLine("Pressione ENTER para retornar ao menu...");
            Console.ReadKey();
            Menu();
        }


        static void ExcluirChamada()
        {
            Console.Clear();
            Console.WriteLine("Excluir Chamada");
            Console.WriteLine("-------------------");

            Console.Write("Digite o número do ticket: ");
            int ticketNumber = Convert.ToInt32(Console.ReadLine());

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM registro_de_chamada WHERE ticket = @ticket";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ticket", ticketNumber);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Chamada excluída com sucesso...");
                        }
                        else
                        {
                            Console.WriteLine("Ticket não encontrado...");

                            Console.WriteLine("O número digitado está correto?");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir chamada: " + ex.Message);
            }
            Console.WriteLine("              " +
                "    ");
            Console.WriteLine("Pressione ENTER para retornar ao menu...");
            Console.ReadKey();
            Menu();
        }

    }
}