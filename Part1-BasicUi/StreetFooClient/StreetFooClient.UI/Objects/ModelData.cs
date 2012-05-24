//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace StreetFooClient.UI
//{
//    // @mbrit - 2012-05-22 - ad hoc data model. better than the one baked into LayoutAwarePage, 
//    // but not as good as a proper view-model...
//    internal class ModelData : Dictionary<string, object>, INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        internal ModelData()
//        {
//        }


//        private void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            if (this.PropertyChanged != null)
//                this.PropertyChanged(this, e);
//        }
//    }
//}
