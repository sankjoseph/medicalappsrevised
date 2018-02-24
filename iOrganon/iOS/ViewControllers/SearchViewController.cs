using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;


namespace iOrganon.iOS
{
    class SearchDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL");

        public SearchDataSource(DBMgr dbMgr)
        {
            
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return DBMgr.Instance.FilteredIntros.Count;
            }

            if (section == 1)
            {
                return DBMgr.Instance.FilteredSummaries.Count;
            }

            if (section == 2)
            {
                return DBMgr.Instance.FilteredChapters.Count;
            }
            return 0;
        }
        public override nint NumberOfSections(UITableView tableView) => 3;
        public override string TitleForHeader(UITableView tableView, nint section)
        {
            if (section ==0 && DBMgr.Instance.FilteredIntros.Count > 0)
            {
                return "Introduction";
            }
            if (section == 1 && DBMgr.Instance.FilteredSummaries.Count > 0)
            {
                return "Summary";
            }
            if (section == 2 && DBMgr.Instance.FilteredChapters.Count > 0)
            {
                return "Chapters";
            }

            return "";
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }

            if (indexPath.Section ==0)
            {
                var item = DBMgr.Instance.FilteredIntros[indexPath.Row];
                cell.TextLabel.Text = item.Id;
                cell.DetailTextLabel.Text = item.Para;
            }

            if (indexPath.Section == 1)
            {
                var item = DBMgr.Instance.FilteredSummaries[indexPath.Row];
                cell.TextLabel.Text = item.Chapters;
                cell.DetailTextLabel.Text = item.Summary;
            }
            if (indexPath.Section == 2)
            {
                var item = DBMgr.Instance.FilteredChapters[indexPath.Row];
                cell.TextLabel.Text = item.Id;
                cell.DetailTextLabel.Text = item.Reading;
            }
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;

            return cell;
        }
    }

    public partial class SearchViewController : UITableViewController
    {
 

        public SearchViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToIntroDetailSegue")
            {
                IntroDetailViewController controller = segue.DestinationViewController as IntroDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);

                if (indexPath.Section == 0)
                {
                    Utils.itemReadingSelected = DBMgr.Instance.FilteredIntros[indexPath.Row].Para;
                }
 
                if (indexPath.Section == 1)
                {
                    Utils.itemReadingSelected = DBMgr.Instance.FilteredSummaries[indexPath.Row].Summary;
                }
                if (indexPath.Section == 2)
                {
                    Utils.itemReadingSelected = DBMgr.Instance.FilteredChapters[indexPath.Row].Reading;
                }
                controller.nCurrentItemIndex = 0;
                controller.bSearch = true;
                controller.bIntroduction = false;
                TableView.DeselectRow(indexPath, true);
            }

        }
        public void performSearch(string searchText)         {
            Utils.searchString = searchText;
             IEnumerable<IntroRead> query =                 (from item in DBMgr.Instance.IntroItems
                 where item.Para.ToUpper().Contains(searchText.ToUpper())
                select item);              DBMgr.Instance.FilteredIntros = query.ToList(); 
            IEnumerable<SummaryRead> querys =
                (from item in DBMgr.Instance.SummaryItems
                 where item.Summary.ToUpper().Contains(searchText.ToUpper())
                 select item);

            DBMgr.Instance.FilteredSummaries = querys.ToList();

            IEnumerable<ChapterRead> querycs =
                (from item in DBMgr.Instance.ChapterItems
                 where item.Reading.ToUpper().Contains(searchText.ToUpper())
                 select item);

            DBMgr.Instance.FilteredChapters = querycs.ToList();             //ItemsDataSource sample = (ItemsDataSource)TableView.Source;             //sample.SetItemsDataSource(FilteredItems);
             TableView.ReloadData();         }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
  
            TableView.Source = new SearchDataSource(DBMgr.Instance);
            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Search";


               TableView.ReloadData();              MySearchBar.BecomeFirstResponder();              MySearchBar.SearchButtonClicked += (sender, e) =>             {                 MySearchBar.ResignFirstResponder();                 string text = MySearchBar.Text.Trim();                 performSearch(text);              };             MySearchBar.TextChanged += (sender, e) =>             {                 string text = e.SearchText;

                                     performSearch(text);             }; 
           // TableView.RegisterNibForCellReuse(SummaryTableViewCell.Nib, "ITEM_CELL");

        }
    }

  
}