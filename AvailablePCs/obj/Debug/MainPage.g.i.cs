﻿

#pragma checksum "E:\Projects\Mobile Dawg\AvailablePCs\AvailablePCs\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D00AE637A30F9B79D7D27E3CDC306D3F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AvailablePCs
{
    partial class MainPage : global::AvailablePCs.LayoutAwarePage
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::AvailablePCs.LayoutAwarePage _apcLayoutAwarePage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Data.CollectionViewSource cvsLabs; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Data.CollectionViewSource cvsLabsInfo; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Grid SemanticGrid; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ProgressRing loading_ring; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock loading; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.SemanticZoom semanticLandscapeZoom; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.SemanticZoom semanticPortraitZoom; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ScrollViewer portraitScrollViewer; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView zoomedInPortraitGridView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView zoomedOutPortraitGridView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView zoomedInLandscapeGridView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView zoomedOutLandscapeGridView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.VisualStateGroup ApplicationViewStates; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.VisualState FullScreenLandscape; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.VisualState FullScreenPortrait; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.VisualState Filled; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.VisualState Snapped; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///MainPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            _apcLayoutAwarePage = (global::AvailablePCs.LayoutAwarePage)this.FindName("_apcLayoutAwarePage");
            cvsLabs = (global::Windows.UI.Xaml.Data.CollectionViewSource)this.FindName("cvsLabs");
            cvsLabsInfo = (global::Windows.UI.Xaml.Data.CollectionViewSource)this.FindName("cvsLabsInfo");
            SemanticGrid = (global::Windows.UI.Xaml.Controls.Grid)this.FindName("SemanticGrid");
            loading_ring = (global::Windows.UI.Xaml.Controls.ProgressRing)this.FindName("loading_ring");
            loading = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("loading");
            semanticLandscapeZoom = (global::Windows.UI.Xaml.Controls.SemanticZoom)this.FindName("semanticLandscapeZoom");
            semanticPortraitZoom = (global::Windows.UI.Xaml.Controls.SemanticZoom)this.FindName("semanticPortraitZoom");
            portraitScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)this.FindName("portraitScrollViewer");
            zoomedInPortraitGridView = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("zoomedInPortraitGridView");
            zoomedOutPortraitGridView = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("zoomedOutPortraitGridView");
            zoomedInLandscapeGridView = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("zoomedInLandscapeGridView");
            zoomedOutLandscapeGridView = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("zoomedOutLandscapeGridView");
            ApplicationViewStates = (global::Windows.UI.Xaml.VisualStateGroup)this.FindName("ApplicationViewStates");
            FullScreenLandscape = (global::Windows.UI.Xaml.VisualState)this.FindName("FullScreenLandscape");
            FullScreenPortrait = (global::Windows.UI.Xaml.VisualState)this.FindName("FullScreenPortrait");
            Filled = (global::Windows.UI.Xaml.VisualState)this.FindName("Filled");
            Snapped = (global::Windows.UI.Xaml.VisualState)this.FindName("Snapped");
        }
    }
}


