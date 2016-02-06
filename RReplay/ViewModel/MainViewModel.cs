﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RReplay.Model;
using RReplay.Properties;
using RReplay.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace RReplay.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IReplayRepository _dataService;
        ObservableCollection<Replay> _Replays;


        public const string SelectedReplayPropertyName = "SelectedReplay";
        private Replay _selectedReplay;

        public RelayCommand<CancelEventArgs> WindowClosingCommand { get; private set; }


        private RelayCommand<string> _copyDeckCodeCommand;
        public RelayCommand<string> CopyDeckCodeCommand
        {
            get
            {
                return _copyDeckCodeCommand ?? (_copyDeckCodeCommand = new RelayCommand<string>( (value) =>
                {
                    Clipboard.SetData(DataFormats.Text, value);
                }));
            }
        }

        private RelayCommand<ulong> _openSteamCommunityPageCommand;
        public RelayCommand<ulong> OpenSteamCommunityPageCommand
        {
            get
            {
                return _openSteamCommunityPageCommand ?? (_openSteamCommunityPageCommand = new RelayCommand<ulong>(( value ) =>
                {
                    string uri = @"http://steamcommunity.com/profiles/" + value.ToString();
                    System.Diagnostics.Process.Start(uri);
                }));
            }
        }

        //OpenSteamCommunityPage
        private RelayCommand<string> _browseToReplayFileCommand;
        public RelayCommand<string> BrowseToReplayFileCommand
        {
            get
            {
                return _browseToReplayFileCommand ?? (_browseToReplayFileCommand = new RelayCommand<string>(( value ) =>
                {
                    Process.Start("explorer.exe", string.Format("/select,\"{0}\"", value));
                }));
            }
        }

        private RelayCommand _openReplayJSONView;
        public RelayCommand OpenReplayJSONView
        {
            get
            {
                return _openReplayJSONView ?? (_openReplayJSONView = new RelayCommand( () =>
                {
                    ReplayJSONView replayJSONView = new ReplayJSONView();
                    replayJSONView.Show();
                }));
            }
        }

        public ObservableCollection<Replay> Replays
        {
            get
            {
                return _Replays;
            }
            set
            {
                Set(ref _Replays, value);
            }
        }

        private RelayCommand _refreshReplays;
        public RelayCommand RefreshReplay
        {
            get
            {
                return _refreshReplays ?? (_refreshReplays = new RelayCommand(() =>
                {
                    _dataService.GetData(ReceiveData);
                }));
            }
        }

        public MainViewModel( IReplayRepository dataService )
        {
            // Window Closing
            WindowClosingCommand = new RelayCommand<CancelEventArgs>( (args) =>
            {
                Settings.Default.Save();
            });

            _dataService = dataService;
            _dataService.GetData(ReceiveData);

            if ( IsInDesignMode )
            {
                SelectedReplay = Replays[1];
            }
        }

        private void ReceiveData( ObservableCollection<Replay> item, Exception error)
        {
            if ( error != null )
            {
                if ( error.Message == "EmptyReplaysPath" )
                {
                    // set the empty list anyway
                    _Replays = item;
                    string newPath;
                    if ( ReplayFolderPicker.GetNewReplayFolder(Settings.Default.replaysFolder,out newPath) )
                    {
                        Settings.Default.replaysFolder = newPath;
                        _dataService.GetData(ReceiveData);
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
             else
            {
                Replays = item;
            }
        }

        public Replay SelectedReplay
        {
            get
            {
                return _selectedReplay;
            }
            set
            {
                Set(SelectedReplayPropertyName, ref _selectedReplay, value);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}