using ProjetoLoja.Api;
using ProjetoLoja.Dao;
using ProjetoLoja.Dto;
using Refit;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ProjetoLoja
{
    class Program
    {
        enum menu {Cadastro =1,Alterar,Deletar,Consultar,ConsultaCep,RelatorioPorSexo,Sair =0}
        static async Task Main(string[] args)
        {
            ClienteDao clienteDao = new ClienteDao();
            bool escolherSair = false;

            Console.WriteLine("                               SISTEMA");
            Console.WriteLine("==============================================================================");
            
            //loop para manter o menu vivo
            while (!escolherSair)
            {
                //Tratamento do Erro para quando entrar com Caracter invalidas
                try
                {
                    Console.WriteLine("Escolha uma Opção\n");
                    Console.WriteLine("1-Cadastro\n2-Alterar\n3-Deletar\n4-Consultar\n5-ConsultaCep\n6-RelátorioPorSexo\n0-Sair\n");
                    int esco = int.Parse(Console.ReadLine());
                    menu opcao = (menu)esco;
                    //faz uma verificao em relacao ao menu, se nao for nenhuma delas ele mostra uma mensagem 
                    if (esco == 1 || esco == 2 || esco == 3 || esco == 4 || esco == 5 || esco == 6 || esco == 0)
                    {
                        //Cria as opçoes para o usuario
                        switch (opcao)
                        {
                            case menu.Cadastro:
                                Console.Clear();
                                Console.WriteLine("Digite seu nome: ");
                                string nomeC = Console.ReadLine();
                                Console.WriteLine("Digite seu Endereço: ");
                                string enderecoC = Console.ReadLine();
                                Console.WriteLine("Digite seu Email: ");
                                string emailC = Console.ReadLine();
                                Console.WriteLine("Digite seu Telefone: ");
                                string telefoneC = Console.ReadLine();
                                Console.WriteLine("Digite seu Sexo: ");
                                string sexoC = Console.ReadLine();
                                
                                //verifica se os campos estão em branco, se tiver algum ele não deixa gravar
                                if(nomeC == "" || enderecoC == "" || emailC == "" || telefoneC == "" || sexoC == "")
                                {
                                    Console.WriteLine("Informação em branco, tente novamente");
                                }
                                else
                                {
                                    ClienteDto clienteC = new ClienteDto
                                    {
                                        Nome = nomeC,
                                        Endereco = enderecoC,
                                        Email = emailC,
                                        Telefone = telefoneC,
                                        Sexo = sexoC
                                    };
                                    clienteDao.InserirCliente(clienteC);
                                }
                                break;

                            case menu.Alterar:
                                Console.Clear();
                                Console.WriteLine("Digite o Id que deseja alterar: ");
                                int idA = int.Parse(Console.ReadLine());
                                //verifica se o id existe no banco de dados
                                bool veri = clienteDao.VerificaId(idA);
                                //se for true entao o id nao existe
                                if(veri == true)
                                {
                                    Console.WriteLine("Não é possivel alterar, id não existe");
                                }
                                else
                                {
                                    Console.WriteLine("Digite seu nome: ");
                                    string nomeA = Console.ReadLine();
                                    Console.WriteLine("Digite seu Endereço: ");
                                    string enderecoA = Console.ReadLine();
                                    Console.WriteLine("Digite seu Email: ");
                                    string emailA = Console.ReadLine();
                                    Console.WriteLine("Digite seu Telefone: ");
                                    string telefoneA = Console.ReadLine();
                                    Console.WriteLine("Digite seu Sexo: ");
                                    string sexoA = Console.ReadLine();

                                    //verifica se os campos estão em branco, se tiver ele não deixa gravar
                                    if (nomeA == "" || enderecoA == "" || emailA == "" || telefoneA == "" || sexoA == "")
                                    {
                                        Console.WriteLine("Informação em branco, tente novamente");
                                    }
                                    else
                                    {
                                        ClienteDto clienteA = new ClienteDto
                                        {
                                            id = idA,
                                            Nome = nomeA,
                                            Endereco = enderecoA,
                                            Email = enderecoA,
                                            Telefone = telefoneA,
                                            Sexo = sexoA
                                        };
                                        clienteDao.AlteraCliente(clienteA);
                                    }
                                }
                                break;

                            case menu.Deletar:
                                Console.Clear();
                                Console.WriteLine("Digite o Id que deseja Deletar: ");
                                int idD = int.Parse(Console.ReadLine());
                                clienteDao.DeletarCliente(idD);
                                break;

                            case menu.Consultar:
                                Console.Clear();
                                clienteDao.MostrarClientes();
                                break;

                                //api consulta CEP
                            case menu.ConsultaCep:
                                Console.Clear();
                                try
                                {
                                    var cepClient = RestService.For<ICepApiService>("http://viacep.com.br");
                                    Console.WriteLine("Digite seu CEP: ");

                                    string cepInformado = Console.ReadLine().ToString();
                                    Console.WriteLine("Consultando informações do CEP: " + cepInformado);

                                    var adress =  await cepClient.GetAddressAsnyc(cepInformado);
                                    Console.WriteLine($"\nLogradouro: {adress.Logradouro}\nBairro: {adress.Bairro}\nCidade: {adress.Localidade}\nUF: {adress.Uf}");
                                    Console.ReadKey();
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro na consulta de cep, cep inválido");
                                }
                                Console.ReadKey();
                                break;

                            case menu.RelatorioPorSexo:
                                Console.Clear();
                                Console.WriteLine("Digite o sexo, masculino ou feminino");
                                string sex = Console.ReadLine();
                                if(sex != "masculino" || sex != "feminino")
                                {
                                    Console.WriteLine("Sexo digitado inválido");
                                }
                                else
                                {
                                    clienteDao.relatorioPorSexo(sex);
                                }
                                break;

                            case menu.Sair:
                                escolherSair = true;
                                break;
                        }
                    }
                    //caso a opcao escolhida nao existir vai cair aqui e voltar ao loop
                    else
                    {
                        Console.WriteLine("Opções Inválidas");
                    }
                }
                //pega o erro de digitacao ex(letras e caracter invalidos)
                catch (System.FormatException)
                {
                    Console.WriteLine("caractere inserido inválido , Tente Novamente\n");
                }
            
             
            }


        }
    }
}
