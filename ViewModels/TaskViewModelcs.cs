using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TodoApp.ViewModels
{
    public class TaskViewModel : BindableObject
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();

        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }

        private string _newTaskDescription;
        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set
            {
                _newTaskDescription = value;
                OnPropertyChanged();
            }
        }

        private DateTime _newTaskDueDate = DateTime.Today;
        public DateTime NewTaskDueDate
        {
            get => _newTaskDueDate;
            set
            {
                _newTaskDueDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        public TaskViewModel()
        {
            AddTaskCommand = new Command(AddTask);
            RemoveTaskCommand = new Command<TaskItem>(RemoveTask);
        }

        private void AddTask()
        {
            if (!string.IsNullOrEmpty(NewTaskTitle) && !string.IsNullOrEmpty(NewTaskDescription))
            {
                Tasks.Add(new TaskItem
                {
                    Title = NewTaskTitle,
                    Description = NewTaskDescription,
                    DueDate = NewTaskDueDate,
                    IsCompleted = false
                });

                // Clear input fields after adding task
                NewTaskTitle = string.Empty;
                NewTaskDescription = string.Empty;
                NewTaskDueDate = DateTime.Today;
            }
        }

        private void RemoveTask(TaskItem task)
        {
            if (Tasks.Contains(task))
            {
                Tasks.Remove(task);
            }
        }
    }

    public class TaskItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
