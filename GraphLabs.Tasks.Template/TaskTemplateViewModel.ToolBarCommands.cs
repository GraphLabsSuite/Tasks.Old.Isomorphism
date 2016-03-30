using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Castle.Core.Internal;
using GraphLabs.CommonUI.Controls.ViewModels;
using GraphLabs.Graphs;

namespace GraphLabs.Tasks.Template
{
    public partial class TaskTemplateViewModel
    {
        #region Полезности

        private const string ImageResourcesPath = @"/GraphLabs.Tasks.Template;component/Images/";

        private Uri GetImageUri(string imageFileName)
        {
            return new Uri(ImageResourcesPath + imageFileName, UriKind.Relative);
        }

        #endregion

        private void InitToolBarCommands()
        {
            new HelpDialog(Strings.Strings_RU.stage3Help).Show();

            #region Третий этап
            #region Видимость

            var vis = true;

            var phase3VisCommand = new ToolBarInstantCommand(
                () =>
                {
                    WorkspaceGraphVisibility = vis ? Visibility.Visible : Visibility.Collapsed;
                    vis = !vis;
                },
                () => _state == State.Nothing
                )
            {
                Description = "Скрыть / Показать слой зелёного графа",
                Image = new BitmapImage(GetImageUri("Info.png"))
            };
            #endregion

            #region Справка
            var phase3HelpCommand = new ToolBarInstantCommand(
                () => new HelpDialog(Strings.Strings_RU.stage3Help).Show(),
                () => _state == State.Nothing
                )
            {
                Description = Strings.Strings_RU.buttonHelp,
                Image = new BitmapImage(GetImageUri("Info.png"))
            };
            #endregion

            #region Завершить этап
            var phase3Command = new ToolBarInstantCommand(
                () =>
                {
                    if (WorkspaceGraph.Vertices[0].Name != "True")
                    {
                        UserActionsManager.RegisterMistake(Strings.Strings_RU.stage3Mistake1, 10);
                    }
                    else
                    {
                        UserActionsManager.RegisterInfo(Strings.Strings_RU.stage3Done);
                    }
                },
                () => _state == State.Nothing
                )
            {
                Image = new BitmapImage(GetImageUri("Check.png")),
                Description = Strings.Strings_RU.stage3DoneButtonDisc
            };
            #endregion
            #endregion
            
            Phase3ToolBarCommands = new ObservableCollection<ToolBarCommandBase>
            {
                phase3VisCommand,
                phase3Command,
                phase3HelpCommand
            };
        }
    }
}
