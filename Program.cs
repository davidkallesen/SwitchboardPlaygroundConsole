using SwitchboardPlaygroundConsole;

var hmax = 4;
var vmax = 3;

var switchboard = SwitchboardFactory.Create(hmax, vmax);

SwitchboardHelper.SetOccupied(switchboard, 1, 0);
SwitchboardHelper.SetInOut(switchboard, 1, 1, 0, 5);

SwitchboardHelper.Render(switchboard, hmax, vmax);