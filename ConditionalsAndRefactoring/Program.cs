using System;
using System.Collections.Generic;
using FeatureFactory;

namespace ConditionalsAndRefactoring
{
	public class Program
	{
		static void Main(string[] args)
		{
			var ooc = new OutOfControlConditionals();
			ooc.ProcessData();

			Console.WriteLine();

			var cf = new CleanRefactor();
			cf.ProcessData();

		}
	}

	public static class MockDataSet
	{
		public static List<Feature> list = new List<Feature> 
		{
			new Feature {FeatureType = 1, Status = 2},	
			new Feature {FeatureType = 2, Status = 1},
			new Feature {FeatureType = 3, Status = 1}
		};
	}

	public class OutOfControlConditionals				// Each class would be in its own file of course but the example here is to illustrate
	{													// a handy refactoring, not to make you read thru a dozen files.
		public void ProcessData()
		{
			foreach(Feature f in MockDataSet.list)
			{
				if(f.FeatureType == 1 && f.Status != 2)
				{
					//do some processing
					Console.WriteLine( "Processing FeatureType {0}", f.FeatureType );
				}
				else if (f.FeatureType == 3 && (f.Status == 1 || f.Status == 3))
				{
					//do the 3 processing
					Console.WriteLine( "Processing FeatureType {0}", f.FeatureType );
				}
				else
				{
					//do default processing
					Console.WriteLine( "Processing Default FeatureType {0}", f.FeatureType );
				}
			}
			Console.WriteLine( "Note FeatureType 1 and 2 are both handled as Defaults, is that even what was intended?" );
		}
	}

	public class CleanRefactor
	{
		public void ProcessData()
		{
			foreach (Feature f in MockDataSet.list)
			{
				IFeature myFeature = MyFeatureFactory.GetFeature( f );
				myFeature.DoProcess();
			}
		}
	}

}
