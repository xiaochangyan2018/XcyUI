using XchyUI.Demo.elmedemo.description;
using XchyUI.widgets;
using XchyUIDemo.elmedemo.description;

namespace XchyUI.Demo.elmedemo
{
    public static class DescriptionManager
    {
        private readonly static List<DescriptionInfo> _infos = new();
        public readonly static XState<int> ScolledToIndexForContent = new XState<int>(-1);
        public static List<DescriptionInfo> GetInfos()
        {
            if(_infos.Count == 0)
            {
                _infos.AddRange(TextDescription.GetInfos());
                _infos.AddRange(ButtonDescription.GetInfos());
                _infos.AddRange(GroupDescription.GetInfos());
                _infos.AddRange(FormDescription.GetInfos());
                _infos.AddRange(TreeViewDescription.GetInfos());
                _infos.AddRange(TreeGridDescription.GetInfos());
                _infos.AddRange(DataGridDescription.GetInfos());
                _infos.AddRange(PopoverDescription.GetInfos());
                _infos.AddRange(ChartDescription.GetInfos());
            }
            return _infos;
        }

        public static void ToIndexForContent(string tag)
        {
            var info = _infos.FirstOrDefault(n => n.Tag!=null && tag.ToLower().StartsWith(n.Tag.ToLower()));
            if (info != null)
            {
                ScolledToIndexForContent.Send(_infos.IndexOf(info));
            }
        }
    }

}
