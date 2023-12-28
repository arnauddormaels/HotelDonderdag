using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.mappers;
using Hotel.Presentation.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EventManager = Hotel.Domain.Managers.EventManager;

namespace Hotel.Presentation.WindowsOrganisor
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {

        public EventUI eventUI { get; set; }
        public EventManager eventManager { get; set; }
        public DescriptionManager descriptionManager;
        public PriceInfoManager priceInfoManager;
        private ObservableCollection<DescriptionUI> descriptionUIs = new ObservableCollection<DescriptionUI>();
        private ObservableCollection<PriceInfoUI> priceInfoUIs = new ObservableCollection<PriceInfoUI>();


        private DateTime fixture;
        public EventWindow(EventManager eventManager)
        {
            InitializeComponent();
            descriptionManager = new DescriptionManager(RepositoryFactory.DescriptionRepository);
            priceInfoManager = new PriceInfoManager(RepositoryFactory.PriceInfoRepository);
            descriptionUIs = new ObservableCollection<DescriptionUI>(descriptionManager.GetDescriptions().Select(description => DescriptionMapper.ToDescriptionUI(description)).ToList());
            priceInfoUIs = new ObservableCollection<PriceInfoUI>(priceInfoManager.getPriceInfos().Select(priceInfo => PriceInfoMapper.ToPriceInfoUI(priceInfo)).ToList());

            DescriptionDataGrid.ItemsSource = descriptionUIs;
            PriceInfoDataGrid.ItemsSource = priceInfoUIs;
            this.eventManager = eventManager;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (DescriptionDataGrid.SelectedItem == null || PriceInfoDataGrid.SelectedItem == null)
            {

                MessageBox.Show("Please select a description and a price info");
            }
            else if (string.IsNullOrEmpty(NrOfPlacesTextBox.Text) || string.IsNullOrWhiteSpace(NrOfPlacesTextBox.Text) || !int.TryParse(NrOfPlacesTextBox.Text, out int temp))
            {

                MessageBox.Show("Please enter a valid nr of places.");
            }
            else if (IsFormatValid())   //2024-11-19 12:30:00.000
            {

                if (eventUI == null)
                {
                    //Nieuw
                    //wegschrijven

                    int nrOfPlaces = int.Parse(NrOfPlacesTextBox.Text);
                    PriceInfoUI priceInfo = (PriceInfoUI)PriceInfoDataGrid.SelectedItem;
                    DescriptionUI description = (DescriptionUI)DescriptionDataGrid.SelectedItem;
                    eventUI = new EventUI(fixture, nrOfPlaces, priceInfo, description);
                }

                else
                {
                    //Update
                    //update DB
                    eventUI.Fixture = fixture;
                    eventUI.NrOfPlaces = int.Parse(NrOfPlacesTextBox.Text);
                    eventUI.PriceInfo = (PriceInfoUI)PriceInfoDataGrid.SelectedItem;
                    eventUI.Description = (DescriptionUI)DescriptionDataGrid.SelectedItem;

                }
                DialogResult = true;

                Close();
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool IsFormatValid()
        {
            //fixture = DateTime.Parse("2023 - 11 - 19 12:30:00.000");
            if (DateTime.TryParse(FixTimeTextBox.Text, out fixture))
            {
                if (fixture < DateTime.Now)
                {
                    MessageBox.Show("fix time must be in the future.", "Error");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Invalid fix time format. Please enter a valid date.", "Error");
                return false;

            }

        }


        private void MenuItemAddDescription_Click(object sender, RoutedEventArgs e)
        {
            DescriptionWindow w = new DescriptionWindow();
            if (w.ShowDialog() == true)
            {
                try
                {

                    w.DescriptionUI.Id = descriptionManager.AddDescription(DescriptionMapper.ToDescription(w.DescriptionUI));
                    descriptionUIs.Add(w.DescriptionUI);
                    DescriptionDataGrid.Items.Refresh();
                    DescriptionDataGrid.SelectedItem = w.DescriptionUI;


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }

            }

        }
        private void MenuItemDeleteDescription_Click(object sender, RoutedEventArgs e)
        {
            if (DescriptionDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                descriptionManager.DeleteDescription(((DescriptionUI)DescriptionDataGrid.SelectedItem).Id);
                descriptionUIs.Remove((DescriptionUI)DescriptionDataGrid.SelectedItem);
            }
        }

        private void MenuItemAddPriceInfo_Click(object sender, RoutedEventArgs e)
        {
            PriceInfoWindow w = new PriceInfoWindow();
            if (w.ShowDialog() == true)
            {
                try
                {
                    //Een controle of de member al bestaat.
                    w.PriceInfoUI.Id = priceInfoManager.AddPriceInfo(PriceInfoMapper.ToPriceInfo(w.PriceInfoUI));
                    priceInfoUIs.Add(w.PriceInfoUI);
                    PriceInfoDataGrid.Items.Refresh();
                    PriceInfoDataGrid.SelectedItem = w.PriceInfoUI;


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }

            }
        }

        private void MenuItemDeletePriceInfo_Click(object sender, RoutedEventArgs e)
        {
            if (PriceInfoDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                priceInfoManager.DeletePriceInfo(((PriceInfoUI)PriceInfoDataGrid.SelectedItem).Id);
                priceInfoUIs.Remove((PriceInfoUI)PriceInfoDataGrid.SelectedItem);
            }
        }

    }
}
