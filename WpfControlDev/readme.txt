说明：
 asyncUI 
	基于.net45的异步ui线程安全操作

Student.cs
	DelegateCommand : ICommand
	ViewModelBase : INotifyPropertyChanged

FastKey.cs
	将window的非绑定属性InputBindingsSource注册为依赖绑定

CodeSeach.xaml
	高效搜索组件，使用ViewSource和Filter实现
	
	在VM中获取viewsource ：
	view中 ItemSources直接绑定StudentList
	ICollectionView viewsource = CollectionViewSource.GetDefaultView(StudentList);
	viewsource.Filter = (obj)=>{ /*filter*/; return filter result; };

ScoreListView.xaml
	分组实现

PwdWin.xaml :
	PasswordBoxHelper
	Pwd绑定
	WaterMark

WinCanvas.xaml :
	Canvas
	ZIndex
	ViewBox

WinRadioStyle.xaml
	使用RadioButton实现TabControl的TabItem效果

WinDataGridComboxEdit.xaml
	实现datagrid中编辑Combox