using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Data.SqlClient;
//using System.Data.MySqlClient;
using MySql.Data.MySqlClient;
namespace EtatCommandes
{
    class Etat
    {

        static void Main(string[] args)
        {
            // MySqlConnection dataConnexion = new MySqlConnection();
            string connectionString = "SERVER=127.0.0.1; DATABASE=northwind; UID=root; PASSWORD=";
            MySqlConnection dataConnexion = new MySqlConnection(connectionString);
            string reponse = " ";
            bool continuer = true;

            while (continuer)
            {

                try
                {


                    dataConnexion.Open();
                    Console.Write("Entrez un code client SVP 5 Car. : ");
                    String customerID = Console.ReadLine();
                    MySqlCommand dataCommand = new MySqlCommand();
                    dataCommand.Connection = dataConnexion;

                    dataCommand.CommandText = "Select OrderID,OrderDate,ShippedDate,Shipname,ShipAddress,ShipCity,ShipCountry " + "From orders Where CustomerID = @CustomerIdParam";
                    MySqlParameter param = new MySqlParameter("@CustomerIdParam", MySqlDbType.VarChar, 5);
                    param.Value = customerID;
                    dataCommand.Parameters.Add(param);
                    Console.WriteLine("Prêt à exécuter la requête : {0}\n\n", dataCommand.CommandText);
                    MySqlDataReader dataReader = dataCommand.ExecuteReader();
                    //Le client n'a pas fait de commande
                    if (!dataReader.Read())
                    {
                        Console.WriteLine("Ce client na pas fait de commande");
                    }

                    while (dataReader.Read())
                    {

                        int orderID = dataReader.GetInt32(0);
                        DateTime dateCommande = dataReader.GetDateTime(1);
                        if (dataReader.IsDBNull(2))
                        {
                            Console.WriteLine("Commande {0} non encore livree " + orderID);
                        }


                        else
                        {
                            DateTime dateLivraison = dataReader.GetDateTime(2);

                            // DateTime dateLivraison = dataReader.GetDateTime(2);
                            String nomLivraison = dataReader.GetString(3);
                            String adrLivraison = dataReader.GetString(4);
                            String villeLivraison = dataReader.GetString(5);
                            String paysLivraison = dataReader.GetString(6);
                            Console.WriteLine("Commande: " + orderID + "\n" + "Placee le  " + dateCommande + "\n" + "Livree le  " + dateLivraison + "\n" + "Adresse:  " + nomLivraison + "\n" + adrLivraison + "\n" + villeLivraison + "\n" + paysLivraison + "\n\n");
                        }
                    }
                    Console.WriteLine("Voulez vous continuer (o/n)");
                    reponse = Console.ReadLine();
                    if (reponse == "n")
                    {
                        continuer = false;
                        Console.WriteLine("Au revoir!");
                    }
                    dataReader.Close();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur accès à la Base Northwind :  {0}  ", e.Message);
                }
                finally
                {
                    dataConnexion.Close();
                }
            }

        }
    }
}
