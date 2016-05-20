using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GraphLabs.CommonUI.Controls.ViewModels;

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
                    vis = !vis;
                    WorkspaceGraphVisibility = vis ? Visibility.Visible : Visibility.Collapsed;
                },
                () => _state == State.Nothing
                )
            {
                Description = "Скрыть / Показать слой зелёного графа",
                Image = new BitmapImage(GetImageUri("HideGreen.png"))
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
                    var dialog = new AnswerDialog(IsomorphismResult);
                    dialog.Closed += (sender, args) =>
                    {
                        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value && dialog.Answer)
                        {
                            UserActionsManager.SendReportOnEveryAction = true;
                            UserActionsManager.RegisterInfo("Ответ пользователя: Графы изоморфны");
                            UserActionsManager.ReportThatTaskFinished();
							TransferToNextTask();
                        }
                        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value && !dialog.Answer)
                        {
                            UserActionsManager.SendReportOnEveryAction = true;
                            UserActionsManager.RegisterInfo("Ответ пользователя: Графы неизоморфны.\n" + dialog.Message);
                            UserActionsManager.ReportThatTaskFinished();
							TransferToNextTask();
                        }
                    };
                    dialog.Show();

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
