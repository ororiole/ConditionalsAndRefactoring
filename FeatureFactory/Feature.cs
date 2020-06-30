using System;

namespace FeatureFactory
{
    public class Feature
    {
		public int FeatureType { get; set; }	// I would normally make these enums but again for the purpose
												// of reviewing a sample lets not make
												// someone read dozens of files just for an illustration
		public int Status { get; set; }
    }

	public interface IFeature
	{
		void DoProcess();
		int Status { get; set; }
		int FeatureType { get; set; }
	}

	public class Feature1 : IFeature
	{
		public Feature1(int status)
		{
			this.Status = status;
			FeatureType = 1;
		}
		#region IFeature members
		public void DoProcess()
		{
			if (Status != 2)	// the single responsibility for status moves inside the interface implementation
			{
				//1 is NOT the default, so now it is handled properly. In this case, it is not processed.
				Console.WriteLine( "Processing FeatureType {0}", FeatureType );
			}
		}
		public int Status { get; set; }
		public int FeatureType { get; set; }
		#endregion
	}

	public class Feature2 : IFeature
	{
		public Feature2(int status)
		{
			this.Status = status;
			FeatureType = 2;
		}

		#region IFeature members
		public void DoProcess()
		{
			//2 is the default, so it is handled properly
			Console.WriteLine( "Processing Default FeatureType {0}", FeatureType );
		}
		public int Status { get; set; }
		public int FeatureType { get; set; }
		#endregion
	}

	public class Feature3 : IFeature
	{
		public Feature3(int status)
		{
			this.Status = status;
			FeatureType = 3;
		}

		#region IFeature members
		public void DoProcess()
		{
			//3 is also handled properly, NOT the default if its status is 2
			if (Status == 1 || Status == 3)
			{
				Console.WriteLine( "Processing FeatureType {0}", FeatureType );
			}
		}
		public int Status { get; set; }
		public int FeatureType { get; set; }
		#endregion
	}

	public static class MyFeatureFactory
	{
		public static IFeature GetFeature(Feature datasetFeature)
		{
			switch (datasetFeature.FeatureType)
			{
				case 1: return new Feature1( datasetFeature.Status );

				case 3: return new Feature3( datasetFeature.Status );

				default: return new Feature2( datasetFeature.Status );
			}
		}
	}


}
