using System;
using System.Collections.Generic;
namespace ConsoleApp9
{
	public class Hashing
	{
		Random rnd = new Random();
		private List<ulong>[] arr;
		ulong a, b, p, m, count, Try;
		public Hashing()
		{
			count = 0;
			Try = 0;
			m = 1;
			p = 1073741824 + (ulong)rnd.Next();
			while (!isPrime(p))
			{
				p = 1073741824 + (ulong)rnd.Next();
			}
			a = ((ulong)rnd.Next() % (p - 1)) + 1;
			b = ((ulong)rnd.Next() % p);
			arr = new List<ulong>[m];
			for(ulong i=0; i < m; i++)
            {
				arr[i] = new List<ulong>();
            }
		}
		public void insert(ulong data)
		{
			doubleArray();
			arr[keyGenerator(data)].Add(data);
			count++;
			//cout << "Size=" << m << "\n";
		}
		public ulong keyGenerator(ulong x)
		{
			return ((((a * x) + b) % p) % m);
		}

		public ulong primeNumberMaker(ulong x)
		{
			ulong num = x + 1;
			while (!isPrime(num))
			{
				num++;
			}
			return num;
		}

		public bool isPrime(ulong x)
		{
			for (ulong i = 2; i <= x / i; ++i)
			{
				if (x % i == 0)
				{
					return false;
				}
			}
			return true;
		}

		public void doubleArray()
		{
			if (count == m)
			{
				ulong size = m;
				List<ulong>[] temp = new List<ulong>[m];
				for (ulong i = 0; i < m; i++)
				{
					temp[i] = new List<ulong>();
				}
				for (ulong i = 0; i < m; i++)
				{
					foreach (ulong element in arr[i])
					{
						temp[i].Add(element);
					}
				}
				m = m * 2;
				p = 1073741824 + (ulong)rnd.Next();
				while (!isPrime(p))
				{
					p = 1073741824 + (ulong)rnd.Next();
				}
				a = ((ulong)rnd.Next() % (p - 1)) + 1;
				b = ((ulong)rnd.Next() % p);
				count = 0;
				arr = new List<ulong>[m];
				for (ulong i = 0; i < m; i++)
				{
					arr[i] = new List<ulong>();
				}
				for (ulong i = 0; i < size; i++)
				{
					foreach (ulong element in temp[i])
					{
						insert(element);
					}
				}
			}
		}
		public void halfArray()
		{
			if (count <= m / 4)
			{
				ulong size = m;
				List<ulong>[] temp = new List<ulong>[m];
				for (ulong i = 0; i < m; i++)
				{
					foreach (ulong element in arr[i])
					{
						temp[i].Add(element);
					}
				}
				m = m / 2;
				p = primeNumberMaker(m);
				a = ((ulong)rnd.Next() % (p - 1)) + 1;
				b = ((ulong)rnd.Next() % p);
				count = 0;
				arr = new List<ulong>[m];
				for (ulong i = 0; i < size; i++)
				{
					foreach (ulong element in temp[i])
					{
						insert(element);
					}
				}
			}
		}
		public void display()
		{
			for (ulong i = 0; i < m; i++)
			{
				Console.Write("Index: " + i + " ==> ");
				foreach (ulong element in arr[i])
				{
					Console.Write(element + "--->");
				}
				Console.Write("\n");
			}
		}

		public void deleteData(ulong data)
		{
			foreach (ulong element in arr[keyGenerator(data)])
			{
				if (element == data)
				{
					arr[keyGenerator(data)].Remove(keyGenerator(data));
					Console.WriteLine(data + "!---!" + "Deleted\n");
					return;
				}
			}
			Console.WriteLine("Element not found. Hence, not deleted\n");
		}

		public void searchData(ulong data)
		{
			foreach (ulong element in arr[keyGenerator(data)])
			{
				if (element == data)
				{
					Console.WriteLine(data + " Found at Index No. " + keyGenerator(data) + "\n");
					return;
				}
			}
			Console.WriteLine("Element doesn't exist\n");
		}

		public ulong collisionsSummary()
		{
			ulong noOfCollisions = 0;
			for (ulong i = 0; i < m; i++)
			{
				if (arr[i].Count > 1)
				{
					noOfCollisions = noOfCollisions + ((ulong)arr[i].Count - 1);
				}
			}
			return noOfCollisions;
		}
		public ulong totalTries()
		{
			return Try;
		}
	}
    public class SecondTable
    {
		private ulong table;
		Random rnd = new Random();
		private List<ulong>[] arr;
		ulong a, b, p, m, count, Try;
		public SecondTable(ulong no)
		{
			table = no;
			count = 0;
			Try = 0;
			m = 1;
			p = 1073741824 + (ulong)rnd.Next();
			while (!isPrime(p))
			{
				p = 1073741824 + (ulong)rnd.Next();
			}
			a = ((ulong)rnd.Next() % (p - 1)) + 1;
			b = ((ulong)rnd.Next() % p);
			arr = new List<ulong>[m];
			for (ulong i = 0; i < m; i++)
			{
				arr[i] = new List<ulong>();
			}
		}
		public void insert(ulong data)
		{
			doubleArray();
			arr[keyGenerator(data)].Add(data);
			count++;
			while (collisionsSummary() != 0)
			{
				Console.WriteLine("Size is=" + collisionsSummary());
				reHash();
				Console.WriteLine("A= " + a + "\n");
				Console.WriteLine("B= " + b + "\n");
				Console.WriteLine("ReTrying for: " + table + " Table\n");
			}
			//cout << "Size=" << m << "\n";
		}
		public void reHash()
		{
			ulong r = (ulong)rnd.Next();
			p = 1073741824 + (ulong)rnd.Next();
			while (!isPrime(p))
			{
				p = 1073741824 + (ulong)rnd.Next();
			}
			Console.WriteLine("Prime Number Selected= " + p);
			r = (ulong)rnd.Next();
			a = (r % (p - 1)) + 1;
			r = (ulong)rnd.Next();
			b = (r % p);
			List<ulong> temp = new List<ulong>();
			for (ulong i = 0; i < m; i++)
			{
				foreach (ulong j in arr[i])
				{
					temp.Add(j);
				}
			}
			arr = new List<ulong>[m];
			for (ulong i = 0; i < m; i++)
			{
				arr[i] = new List<ulong>();
			}
			foreach (ulong k in temp)
			{
				arr[keyGenerator(k)].Add(k);
			}
			Try++;
		}
		public ulong keyGenerator(ulong x)
		{
			return ((((a * x) + b) % p) % m);
		}

		public ulong primeNumberMaker(ulong x)
		{
			ulong num = x + 1;
			while (!isPrime(num))
			{
				num++;
			}
			return num;
		}

		public bool isPrime(ulong x)
		{
			for (ulong i = 2; i <= x / i; ++i)
			{
				if (x % i == 0)
				{
					return false;
				}
			}
			return true;
		}

		public void doubleArray()
		{
			if (count == m)
			{
				ulong size = m;
				List<ulong>[] temp = new List<ulong>[m];
				for (ulong i = 0; i < m; i++)
				{
					temp[i] = new List<ulong>();
				}
				for (ulong i = 0; i < m; i++)
				{
					foreach (ulong element in arr[i])
					{
						temp[i].Add(element);
					}
				}
				m = m * 2;
				p = 1073741824 + (ulong)rnd.Next();
				while (!isPrime(p))
				{
					p = 1073741824 + (ulong)rnd.Next();
				}
				a = ((ulong)rnd.Next() % (p - 1)) + 1;
				b = ((ulong)rnd.Next() % p);
				count = 0;
				arr = new List<ulong>[m];
				for (ulong i = 0; i < m; i++)
				{
					arr[i] = new List<ulong>();
				}
				for (ulong i = 0; i < size; i++)
				{
					foreach (ulong element in temp[i])
					{
						insert(element);
					}
				}
			}
		}
		public void display()
		{
			for (ulong i = 0; i < m; i++)
			{
				Console.Write("Index: " + i + " ==> ");
				foreach (ulong element in arr[i])
				{
					Console.Write(element + "--->");
				}
				Console.Write("\n");
			}
		}


		public void searchData(ulong data)
		{
			ulong[] temp = arr[keyGenerator(data)].ToArray();
			if(temp[0] == data)
            {
				Console.WriteLine(data + " Found at Index No. " + keyGenerator(data) + "\n");
			}
			else
            {
				Console.WriteLine("Element doesn't exist\n");
			}
		}

		public ulong collisionsSummary()
		{
			ulong noOfCollisions = 0;
			for (ulong i = 0; i < m; i++)
			{
				if (arr[i].Count > 1)
				{
					noOfCollisions = noOfCollisions + ((ulong)arr[i].Count - 1);
				}
			}
			return noOfCollisions;
		}
		public ulong totalTries()
		{
			return Try;
		}
	}

    public class FirstTable
	{
		Random rnd = new Random();
		private SecondTable[] arr;
		private ulong a, b, m, p, count;
		public FirstTable(ulong size)
		{
			count = 0;
			m = size;
			arr = new SecondTable[m];
			p = 18446744073709551557;
			a = ((ulong)rnd.Next() % (p - 1)) + 1;
			b = ((ulong)rnd.Next() % p);
			for (ulong i = 0; i < m; i++)
			{
				arr[i] = new SecondTable(i);
			}
		}
		public void insert(ulong data)
		{
			arr[keyGenerator(data)].insert(data);
		}

		public ulong keyGenerator(ulong x)
		{
			return ((((a * x) + b) % p) % m);
		}

		public void display()
		{
			for (ulong i = 0; i < m; i++)
			{
				Console.WriteLine("Index#" + i + "\n");
				arr[i].display();
			}
		}
		public void searchData(ulong data)
		{
			Console.WriteLine("First Talbe Index NO." + keyGenerator(data) + " ");
			arr[keyGenerator(data)].searchData(data);
		}

		public void summaryOfTries()
		{
			Console.WriteLine("~~~~~~ Total Tries ~~~~~~\n");
			for (ulong i = 0; i < m; i++)
			{
				Console.WriteLine("Total Tries from Slot#" + i + " are= " + arr[i].totalTries() + "\n");
				count = count + arr[i].totalTries();
			}
			Console.WriteLine("TOTAL TRIES = " + count + "\n");
		}
	}
	class Program
    {
        static void Main(string[] args)
        {
			Random rnd = new Random();
			ulong size;
            Console.WriteLine("Enter Size of Array");
			size = ulong.Parse(Console.ReadLine());
			FirstTable FT = new FirstTable(size);
			Hashing hash = new Hashing();
			List<ulong> randoms = new List<ulong>();
			for(ulong i=0; i<size; i++)
            {
				ulong rd= (ulong)rnd.Next();
				if (!randoms.Contains(rd))
                {
					randoms.Add(rd);
                }
				else
                {
					Console.WriteLine("Same Random\n");
					i--;
                }
            }
			Console.WriteLine("Enter 1 if you want to do Perfect Hashing");
			Console.WriteLine("Enter 2 if you want to do Simple Hashing");
			int choose = int.Parse(Console.ReadLine());
			if(choose==1)
            {
				for (ulong i = 0; i < size; i++)
				{
					int k = (int)i;
					FT.insert(randoms[k]);
				}
				FT.display();
				FT.summaryOfTries();
				Console.WriteLine("Enter index between 0-" + (size - 1) + " you want to search");
				ulong search = ulong.Parse(Console.ReadLine());
				FT.searchData(randoms[(int)search]);
			}
			if(choose == 2)
            {
				ulong choice;
				for (ulong i = 0; i < size; i++)
				{
					choice = (ulong)rnd.Next() % 3 + 1;
					if (choice == 1)
					{
						int k = (int)i;
						Console.WriteLine("~~~~~ " + randoms[k] + " is being Inserted ~~~~~\n\n\n");
						hash.insert(randoms[k]);
					}
					else if (choice == 2)
					{
						i--;
						if (i == 0)
						{
							Console.WriteLine("~~~~~ " + randoms[0] + " is being Deleted ~~~~~\n");
							hash.deleteData(randoms[0]);
						}
						else if (i > 0)
						{
							ulong Delete = (ulong)rnd.Next() % i;
							int k = (int)Delete;
							Console.WriteLine();
							Console.WriteLine("~~~~~ " + randoms[k] + " is being Deleted ~~~~~\n");
							hash.deleteData(randoms[k]);
						}
						else
						{
							Console.WriteLine("~~~~~ Deleting but No Element in Table Yet ~~~~~\n");
						}
						Console.WriteLine("\n\n");
					}
					else if (choice == 3)
					{
						i--;
						if (i == 0)
						{
							Console.WriteLine("~~~~~ " + randoms[0] + " is being Searched ~~~~~\n");
							hash.searchData(randoms[0]);
						}
						else if (i > 0)
						{
							ulong search = (ulong)rnd.Next() % i;
							int k = (int)search;
							Console.WriteLine();
							Console.WriteLine("~~~~~ " + randoms[k] + " is being Searched ~~~~~\n");
							hash.searchData(randoms[k]);
						}
						else
						{
							Console.WriteLine("~~~~~ Searching but No Element in Table Yet ~~~~~\n");
						}
						Console.WriteLine("\n\n");
					}
				}
				hash.display();
				Console.WriteLine("Total Collisions = " + hash.collisionsSummary());
			}
        }
    }
}
