using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using GraphLabs.Common;
using GraphLabs.Common.Utils;
using GraphLabs.CommonUI;
using GraphLabs.CommonUI.Controls.ViewModels;
using GraphLabs.Graphs;
using GraphLabs.Graphs.DataTransferObjects.Converters;
using GraphLabs.Graphs.UIComponents.Visualization;

namespace GraphLabs.Tasks.Template
{
    /// <summary> ViewModel для TaskTemplate </summary>
    public partial class TaskTemplateViewModel : TaskViewModelBase<TaskTemplate>
    {
        /// <summary> Текущее состояние </summary>
        private enum State
        {
            /// <summary> Пусто </summary>
            Nothing,
        }

        /// <summary> Текущее состояние </summary>
        private State _state;

        /// <summary> Допустимые версии генератора, с помощью которого сгенерирован вариант </summary>
        private readonly Version[] _allowedGeneratorVersions = {  new Version(1, 0) };
        
        /// <summary> Допустимые версии генератора </summary>
        protected override Version[] AllowedGeneratorVersions
        {
            get { return _allowedGeneratorVersions; }
        }


        #region Public свойства вьюмодели

        /// <summary> Идёт загрузка данных? </summary>
        public static readonly DependencyProperty IsLoadingDataProperty = DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.IsLoadingData), 
            typeof(bool), 
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(false));

        /// <summary> Разрешено перемещение вершин? </summary>
        public static readonly DependencyProperty IsMouseVerticesMovingEnabledProperty = DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.IsMouseVerticesMovingEnabled),
            typeof(bool),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(true));

        /// <summary> Разрешено добавление рёбер? </summary>
        public static readonly DependencyProperty IsEdgesAddingEnabledProperty = DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.IsEgesAddingEnabled),
            typeof(bool),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(false));

        /// <summary> Команды панели инструментов</summary>
        public static readonly DependencyProperty Phase3ToolBarCommandsProperty = DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.Phase3ToolBarCommands),
            typeof(ObservableCollection<ToolBarCommandBase>),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(default(ObservableCollection<ToolBarCommandBase>)));

        /// <summary> Выданный в задании граф </summary>
        public static readonly DependencyProperty GivenGraphProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.GivenGraph),
            typeof(IGraph),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(default(IGraph)));

        /// <summary> Изоморфизм </summary>
        public static readonly DependencyProperty IsomorphismResultProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.IsomorphismResult),
            typeof(bool),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(false));
        
        ///<summary> Фоновый граф для изоморфизма </summary>
        public static readonly DependencyProperty BackgroundGraphProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.BackgroundGraph),
            typeof(IGraph),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(default(IGraph)));

        ///<summary> Рабочий граф для изоморфизма </summary>
        public static readonly DependencyProperty WorkspaceGraphProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.WorkspaceGraph),
            typeof(IGraph),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(default(IGraph)));

        ///<summary> Видимость рабочего графа для изоморфизма </summary>
        public static readonly DependencyProperty WorkspaceGraphVisibilityProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((TaskTemplateViewModel m) => m.WorkspaceGraphVisibility),
            typeof(Visibility),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(Visibility.Visible));

        /// <summary> Радиус </summary>
        public static readonly DependencyProperty DefaultVertexRadiusProperty =
            DependencyProperty.Register(
            ExpressionUtility.NameForMember((GraphVisualizer v) => v.DefaultVertexRadius),
            typeof(double),
            typeof(TaskTemplateViewModel),
            new PropertyMetadata(default(double)));

        /// <summary> Идёт загрузка данных? </summary>
        public bool IsLoadingData
        {
            get { return (bool)GetValue(IsLoadingDataProperty); }
            private set { SetValue(IsLoadingDataProperty, value); }
        }

        /// <summary> Разрешено перемещение вершин? </summary>
        public bool IsMouseVerticesMovingEnabled
        {
            get { return (bool)GetValue(IsMouseVerticesMovingEnabledProperty); }
            set { SetValue(IsMouseVerticesMovingEnabledProperty, value); }
        }

        /// <summary> Разрешено добавление рёбер? </summary>
        public bool IsEgesAddingEnabled{
            get { return (bool)GetValue(IsEdgesAddingEnabledProperty); }
            set { SetValue(IsEdgesAddingEnabledProperty, value); }
        }

        /// <summary> Команды панели инструментов </summary>
        public ObservableCollection<ToolBarCommandBase> Phase3ToolBarCommands
        {
            get { return (ObservableCollection<ToolBarCommandBase>)GetValue(Phase3ToolBarCommandsProperty); }
            set { SetValue(Phase3ToolBarCommandsProperty, value); }
        }

        /// <summary> Выданные в задании графы </summary>
        public IGraph GivenGraph
        {
            get { return (IGraph)GetValue(GivenGraphProperty); }
            set { SetValue(GivenGraphProperty, value); }
        }

        /// <summary> Изоморфизм </summary>
        public bool IsomorphismResult => (bool)GetValue(IsomorphismResultProperty);

        /// <summary> Фоновый граф для изоморфизма </summary>
        public IGraph BackgroundGraph
        {
            get { return (IGraph)GetValue(BackgroundGraphProperty); }
            set { SetValue(BackgroundGraphProperty, value); }
        }

        /// <summary> Рабочий граф для изоморфизма </summary>
        public IGraph WorkspaceGraph
        {
            get { return (IGraph)GetValue(WorkspaceGraphProperty); }
            set { SetValue(WorkspaceGraphProperty, value); }
        }

        /// <summary> Видимость рабочего графа </summary>
        public Visibility WorkspaceGraphVisibility
        {
            get { return (Visibility)GetValue(WorkspaceGraphVisibilityProperty); }
            set { SetValue(WorkspaceGraphVisibilityProperty, value); }
        }

        #endregion


        /// <summary> Инициализация </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            UserActionsManager.PropertyChanged += (sender, args) => HandlePropertyChanged(args);
            VariantProvider.PropertyChanged += (sender, args) => HandlePropertyChanged(args);
            InitToolBarCommands();
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            View.Loaded += (sender, args) => StartVariantDownload();
        }

        /// <summary> Начать загрузку варианта </summary>
        public void StartVariantDownload()
        {
            VariantProvider.DownloadVariantAsync();
        }

        private void HandlePropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ExpressionUtility.NameForMember((IUiBlockerAsyncProcessor p) => p.IsBusy))
            {
                // Нас могли дёрнуть из другого потока, поэтому доступ к UI - через Dispatcher.
                Dispatcher.BeginInvoke(RecalculateIsLoadingData);
            }
        }

        private void RecalculateIsLoadingData()
        {
            IsLoadingData = VariantProvider.IsBusy || UserActionsManager.IsBusy;
        }

        /// <summary> Задание загружено </summary>
        /// <param name="e"></param>
        protected override void OnTaskLoadingComlete(VariantDownloadedEventArgs e)
        {
            // Мы вызваны из другого потока. Поэтому работаем с UI-элементами через Dispatcher.
            Dispatcher.BeginInvoke(() =>
            {
                GivenGraph = GraphSerializer.Deserialize(e.Data);
                WorkspaceGraph = GivenGraph;
                BackgroundGraph = GivenGraph;
            });

            //var number = e.Number; -- м.б. тоже где-то показать надо
            //var version = e.Version;
        }
    }
}
