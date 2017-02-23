using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontAssistant
{
    public partial class frmMain : Form
    {
        #region Properties

        private readonly string UniteTTC;
        private readonly bool UniteTTCExists;

        private readonly string Otc2Otf;
        private readonly string Otf2Otc;
        private readonly bool AFDKOExists;

        private readonly string TtfName;
        private readonly bool TtfNameExists;

        private readonly string TmpDir;
        #endregion

        #region Ctor
        public frmMain()
        {
            InitializeComponent();

            TmpDir = Path.Combine(Path.GetTempPath(), "TKFontAssistant");
            try
            {
                if (Directory.Exists(TmpDir))
                    Directory.GetFiles(TmpDir).ToList().ForEach(File.Delete);
            }
            catch
            {
                MessageBox.Show("程序无权对临时文件夹进行读写！", "请注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            var fileName = Process.GetCurrentProcess().MainModule.FileName;
            var cd = Path.GetDirectoryName(fileName);

            UniteTTC = Path.Combine(cd, "UniteTTC.exe");
            UniteTTCExists = File.Exists(UniteTTC);
            tssUniteTTC.ChangeState(UniteTTCExists);

            Otc2Otf = Path.Combine(cd, @"AFDKO\Tools\win\otc2otf.cmd");
            Otf2Otc = Path.Combine(cd, @"AFDKO\Tools\win\otf2otc.cmd");
            AFDKOExists = File.Exists(Otc2Otf) && File.Exists(Otf2Otc);
            tssAFDKO.ChangeState(AFDKOExists);

            TtfName = Path.Combine(cd, "ttfname3_zh.exe");
            TtfNameExists = File.Exists(TtfName);
            tssTtfName.ChangeState(TtfNameExists);

            colFileName.Width = lstLogs.Width - colStatus.Width - colOp.Width;

            foreach (TabPage tabPage in tabMain.TabPages)
            {
                var lbl = new Label
                {
                    Name = tabPage.Name + "Label",
                    Text = $"拖放{tabPage.Tag}文件到此处",
                    AutoSize = false,
                    Tag = tabPage.Tag,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(new FontFamily("Microsoft Yahei UI"), 20),
                    AllowDrop = true
                };
                lbl.DragEnter += Lbl_DragEnter;
                lbl.DragDrop += Lbl_DragDrop;
                lbl.DragLeave += Lbl_DragLeave;
                tabPage.Controls.Add(lbl);
            }
        }
        #endregion

        #region Methods
        private ListViewItem MakeListItem(string text,string op)
        {
            var item = new ListViewItem
            {
                Name = $"opItem{Guid.NewGuid():N}",
                Text = text,
                UseItemStyleForSubItems = false
            };
            item.SubItems.Add(op);
            item.SubItems.Add("正在操作");
            item.SubItems[2].ForeColor = Color.Blue;
            return item;
        }

        private void StartBreakFile(params string[] files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                var dir = Path.GetDirectoryName(file);
                var item = MakeListItem($"{fileNameWithoutExtension}", "拆分");
                lstLogs.Items.Add(item);
                if (splitContainer1.Panel1Collapsed)
                    splitContainer1.Panel1Collapsed = false;
                Task.Run(() =>
                {
                    try
                    {
                        if (dir == null || fileName == null || fileNameWithoutExtension == null) return State.FromError("获取文件名错误");
                        var outDir = $"{dir}\\{fileNameWithoutExtension}";
                        if (Directory.Exists(outDir))
                            Directory.GetFiles(outDir).ToList().ForEach(File.Delete);
                        else
                            Directory.CreateDirectory(outDir);
                        var psi = new ProcessStartInfo
                        {
                            FileName = UniteTTC,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        if (dir.Contains('.'))
                        {
                            var tmpdir = Path.Combine(TmpDir, Guid.NewGuid().ToString("N"));
                            Directory.CreateDirectory(tmpdir);
                            psi.Arguments = $"{Path.Combine(tmpdir, fileNameWithoutExtension.Replace(".", ""))}";
                        }
                        else
                            psi.Arguments = $"{Path.Combine(outDir, fileName)}";
                        if (!File.Exists(psi.Arguments)) File.Copy(file, psi.Arguments);
                        var p = Process.Start(psi);
                        if (p == null) return State.FromError("启动失败");
                        p.WaitForExit(15000);
                        var ret = p.StandardOutput.ReadToEnd();
                        File.Delete(psi.Arguments);
                        if (!ret.Contains("Ok."))
                            return State.FromError("NotOK:" + p.StandardError.ReadToEnd());
                        if (dir.Contains('.'))
                        {
                            var d = Path.GetDirectoryName(psi.Arguments);
                            Directory.GetFiles(d).ToList().ForEach(f => File.Move(f, f.Replace(d, outDir)));
                            Directory.Delete(d);
                        }
                        return State.OK;
                    }
                    catch (Exception e)
                    {
                        return State.FromException(e);
                    }
                }
                ).ContinueWith(task => item.ChangeState(task.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void StartCombineFiles(params string[] files)
        {
            var rgx = new Regex(@"^.*\\.*\d{3}\.[ot]tf$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var destName = rgx.IsMatch(files[0]) ? $"{files[0].Substring(0, files[0].Length - 7)}.ttc" : $"{files[0].Substring(0, files[0].Length - 5)}_combined.ttc";
            var item = MakeListItem($"{Path.GetFileName(destName)}", "合并");
            lstLogs.Items.Add(item);
            if (splitContainer1.Panel1Collapsed)
                splitContainer1.Panel1Collapsed = false;
            Task.Run(() =>
            {
                try
                {
                    if (File.Exists(destName)) File.Delete(destName);
                    //UniteTTC first
                    var psi = new ProcessStartInfo
                    {
                        FileName = UniteTTC,
                        Arguments = $"\"{destName}\" {string.Join(" ", files.Select(c => $"\"{c}\""))}",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    };
                    var p = Process.Start(psi);
                    if (p == null) return State.FromError("启动失败");
                    p.WaitForExit(15000);
                    var ret = p.StandardOutput.ReadToEnd();
                    if (!ret.Contains("Ok."))
                    {
                        var err = p.StandardError.ReadToEnd();
                        if (err.Contains("Not a TTF file"))
                        {
                            if (AFDKOExists)
                            {
                                p = Process.Start(new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = $"/c {Otf2Otc} -o \"{destName}\" {string.Join(" ", files.Select(c => $"\"{c}\""))}",
                                    WorkingDirectory = Otf2Otc.Substring(0, Otf2Otc.Length - 12),
                                    CreateNoWindow = true,
                                    UseShellExecute = false,
                                    RedirectStandardError = true,
                                    RedirectStandardOutput = true
                                });
                                if (p == null) return State.FromError("启动失败");
                                p.WaitForExit(15000);
                                ret = p.StandardOutput.ReadToEnd();
                                if (!ret.Contains("Done"))
                                    return State.FromError("NotOK:" + err);
                            }
                            else
                            {
                                return State.FromError("需要AFDKO工具！");
                            }
                        }
                        else
                        {
                            return State.FromError("NotOK:" + err);
                        }
                    }
                    return State.OK;
                }
                catch (Exception e)
                {
                    return State.FromException(e);
                }
            }
            ).ContinueWith(task => item.ChangeState(task.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void StartGetXML(params string[] files)
        {
            foreach (var file in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                var dir = Path.GetDirectoryName(file);
                var item = MakeListItem($"{fileNameWithoutExtension}", "提取信息");
                lstLogs.Items.Add(item);
                if (splitContainer1.Panel1Collapsed)
                    splitContainer1.Panel1Collapsed = false;
                Task.Run(() =>
                {
                    try
                    {
                        if (dir == null || fileNameWithoutExtension == null) return State.FromError("获取文件名错误");
                        var destName = $"{dir}\\{fileNameWithoutExtension}.xml";
                        if (File.Exists(destName))
                            File.Delete(destName);
                        var p = Process.Start(new ProcessStartInfo
                        {
                            FileName = TtfName,
                            Arguments = $"\"{file}\" -o \"{destName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        });
                        if (p == null) return State.FromError("启动失败");
                        p.WaitForExit();
                        var ret = p.StandardOutput.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(ret))
                            return State.FromError(ret);
                        return State.OK;
                    }
                    catch (Exception e)
                    {
                        return State.FromException(e);
                    }
                }
                ).ContinueWith(task => item.ChangeState(task.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void StartModify(params string[] files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                var dir = Path.GetDirectoryName(file);
                var item = MakeListItem($"{fileNameWithoutExtension}", "修改信息");
                lstLogs.Items.Add(item);
                if (splitContainer1.Panel1Collapsed)
                    splitContainer1.Panel1Collapsed = false;
                Task.Run(() =>
                    {
                        try
                        {
                            if (dir == null || fileName == null || fileNameWithoutExtension == null) return State.FromError("获取文件名错误");
                            var xml = $"{dir}\\{fileNameWithoutExtension}.xml";
                            if(!File.Exists(xml)) return State.FromError("xml文件缺失");
                            var newDir = Path.Combine(dir, "Modified");
                            if (!Directory.Exists(newDir))
                                Directory.CreateDirectory(newDir);
                            var destName = Path.Combine(newDir, fileName);
                            if (File.Exists(destName))
                                File.Delete(destName);
                            var p = Process.Start(new ProcessStartInfo
                            {
                                FileName = TtfName,
                                Arguments = $"\"{xml}\" \"{file}\" -o \"{destName}\"",
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                RedirectStandardOutput = true
                            });
                            if (p == null) return State.FromError("启动失败");
                            p.WaitForExit();
                            var ret = p.StandardOutput.ReadToEnd();
                            if (!string.IsNullOrWhiteSpace(ret))
                                return State.FromError(ret);
                            return State.OK;
                        }
                        catch (Exception e)
                        {
                            return State.FromException(e);
                        }
                    }
                ).ContinueWith(task => item.ChangeState(task.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        #endregion

        #region Events
        private void Lbl_DragLeave(object sender, EventArgs e)
        {
            var s = (Label)sender;
            s.Text = $"拖放{s.Tag}文件到此处";
        }

        private void Lbl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var s = (Label)sender;
                if (files.Any(Directory.Exists))
                {
                    s.Text = "请拖放文件而不是文件夹";
                    return;
                }
                var grpExt = files.GroupBy(Path.GetExtension).ToArray();
                if (grpExt.Length != 1)
                {
                    s.Text = "请保持所有文件的类型相同";
                    return;
                }
                var ext = grpExt[0].Key.ToLower();
                switch (s.Name)
                {
                    case "tpSplitLabel":
                        if (!UniteTTCExists) s.Text = "找不到所需工具：UniteTTC";
                        else if (ext != ".ttc" && ext != ".otc") s.Text = "请拖放正确的文件";
                        else e.Effect = DragDropEffects.Link;
                        break;
                    case "tpCombineLabel":
                        if (ext != ".ttf" && ext != ".otf") s.Text = "请拖放正确的文件";
                        else if (ext == ".ttf" && !UniteTTCExists) s.Text = "找不到所需工具：UniteTTC";
                        else if (ext == ".otf" && !AFDKOExists) s.Text = "找不到所需工具：AFDKO";
                        else e.Effect = DragDropEffects.Link;
                        break;
                    case "tpGetXmlLabel":
                        if (!TtfNameExists) s.Text = "找不到所需工具：ttfname3_zh";
                        else if (ext != ".ttf" && ext != ".otf") s.Text = "请拖放正确的文件";
                        else e.Effect = DragDropEffects.Link;
                        break;
                    case "tpModifyLabel":
                        if (!TtfNameExists) s.Text = "找不到所需工具：ttfname3_zh";
                        else if (ext != ".ttf" && ext != ".otf") s.Text = "请拖放正确的文件";
                        else e.Effect = DragDropEffects.Link;
                        break;
                }

            }
        }

        private void Lbl_DragDrop(object sender, DragEventArgs e)
        {
            var s = (Label)sender;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            switch (s.Name)
            {
                case "tpSplitLabel":
                    StartBreakFile(files);
                    break;
                case "tpCombineLabel":
                    StartCombineFiles(files);
                    break;
                case "tpGetXmlLabel":
                    StartGetXML(files);
                    break;
                case "tpModifyLabel":
                    StartModify(files);
                    break;
            }
        }

        private void tabMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void tabMain_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                var first = files[0];
                if (Directory.Exists(first))
                    return;
                var extName = Path.GetExtension(first)?.ToLower();
                if (extName == null || extName != ".ttf" && extName != ".otf")
                    return;
                var input = Microsoft.VisualBasic.Interaction.InputBox("要变成多少份", "FileCopy", "6");
                int parse;
                if (int.TryParse(input, out parse))
                {
                    var file001 = $"{first.Substring(0, first.Length - 4)}001{extName}";
                    File.Move(first, file001);
                    for (var i = 2; i <= parse; i++)
                        File.Copy(file001, $"{first.Substring(0, first.Length - 4)}{i:D3}{extName}");
                }
            }
        }
        #endregion
    }

    class State
    {
        public State(string message,Color color)
        {
            ResultMessage = message ?? "完成";
            ResultColor = color;
        }

        public string ResultMessage { get; }
        public Color ResultColor { get; }

        public static State OK => new State("完成",Color.Green);

        public static State FromException(Exception ex)
        {
            return new State(ex.Message, Color.Red);
        }
        public static State FromError(string ex)
        {
            return new State(ex, Color.Red);
        }
    }

    static class Extended
    {
        public static void ChangeState(this ListViewItem item, State state)
        {
            item.SubItems[2].Text = state.ResultMessage;
            item.SubItems[2].ForeColor = state.ResultColor;
        }
        public static void ChangeState(this ToolStripStatusLabel item, bool state)
        {
            if (state)
            {
                item.Text = "找到";
                item.ForeColor = Color.Green;
            }
            else
            {
                item.Text = "未找到";
                item.ForeColor = Color.Red;
            }
        }
    }
}
