using Proj1_SampleConApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSERVER
{
    class productTable
    {
        public int prdt_id { get; set; }
        public string prdt_name { get; set; }
        public int vndr_id { get; set; }
        public static object prdt { get; private set; }

    }
    class ProductInventory
    {
        const string STRSELECT = "SELECT * FROM product TABLE";
        const string STRCONNECTION = "Data Source=W-674PY03-2;Initial Catalog=dharini_db;Persist Security Info=True;User ID=SA;Password=Password@123456-=";
        public static object prdt { get; private set; }
        static void readAllRecords()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = STRCONNECTION;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = STRSELECT;
            cmd.Connection = con; //HAS- A RELATION

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID : {reader["prdt_id"]} NAME: {reader["prdt_name"]} VNDRID:{reader["vndr_id"]}");
                    //Console.WriteLine(reader["empName"]);
                    //Console.WriteLine(reader["empAddress"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private static void insertRecord( string name, int vndr)
        {
            string query = $"insert into product values('{name}',{vndr})";
            SqlConnection con = new SqlConnection(STRCONNECTION);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    Console.WriteLine("product added succesfully!");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static List<productTable> getAllEmployees()
        {

            SqlConnection con = new SqlConnection(STRCONNECTION);
            SqlCommand cmd = new SqlCommand(STRSELECT, con);
            List<productTable> records = new List<productTable>();

             try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                       var prdt = new productTable
                       {
                        prdt_id = Convert.ToInt32(reader[0]),
                        prdt_name = reader[1].ToString(),
                        vndr_id= Convert.ToInt32(reader[0]),
                        };
                    records.Add(prdt);
                }
            }
            catch (SqlException ex)
            {
              Console.WriteLine(ex.Message);
            }
            finally
            {
                 con.Close();
            }
             return records;
           }
        private static void updateRecord(int id,productTable prdt)
        {
            string query = $"update productTable Set prdt_name = '{prdt.prdt_name}',vndr_id='{prdt.vndr_id}'";
            SqlConnection con = new SqlConnection(STRCONNECTION);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    Console.WriteLine("employee updated succesfully!");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("PRODUCT INVENTORY");
            Console.WriteLine("1.add product");
            Console.WriteLine("2.display product");
            Console.WriteLine("3.update product");
            Console.WriteLine("enter your choice");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: 
                readAllRecords();
             
                var prdt_name = Utilities.GetString("enter the name of product");
                var vndr_id = Utilities.GetInteger("enter the vndr_id of product");

                insertRecord(prdt_name, vndr_id);
                    break;
                case 2:
                    var records = getAllEmployees();
                    foreach(var prdt in records)
                    Console.WriteLine(prdt.prdt_name + " with product id " + prdt.prdt_id + "and vendor id" + prdt.vndr_id);
                    break;
                case 3:
                    updateRecord( 105);
                    int id = Utilities.GetInteger("Enter Id of the product to be updated");
                    productTable prdtup = new productTable { prdt_id = id, prdt_name = "shampoo", vndr_id = 107 };
                    List<productTable> prdtlist = getAllEmployees();
                    foreach (var prdt in prdtlist)
                     if (prdt.prdt_id == id)
                     updateRecord(id, prdtup);
                    updateRecord(2, productTable);

            }

        }
    }
}
