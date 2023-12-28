// Train track Switchboard layout program
// The purpose of this app is to do auto-layout of train tracks in a n x m grid of cells. 
// The program should be able to layout the tracks in a way that finds the shortest (weighted) path between a start point
// and an end point. Each cell in the grid has 8 connection points (corners and midpoints of each side) that can be connected. 
// The connection points will be numbered from 0 to 7, starting at the top left corner and going clockwise.
// Some cells may already be occupied by a track segment. The program should be able to layout the tracks in a way that finds
// the best path around these "obstacles".
// In the initial version, we are mainly interested in the path algorithm, so the UI is very basic. It will just show the grid
// as ascii text, and I already have the code to draw the result.

// The program will be able to read a text file that describes the grid and the obstacles. 
// Each cell is represented by a char. An empty cell is space space. A blocked cell is a #.
// The start cell has a number from 0 to 7, indicating which of the 8 connection points is the exit point.
// The end cell has a letter from a to h, indicating which of the 8 connection points is the entry point.

// The program will print the grid to the console by calling the SwitchboardHelper.Render(switchboard, hmax, vmax);
// The switchboard is a 2D array of Cell objects. The hmax and vmax are the horizontal and vertical size of the grid.
using SwitchboardPlaygroundConsole;

var hmax = 5;
var vmax = 4;

var startcell = new int[] { 4, 0 };
var targetcell = new int[] { 0, 3 };


var switchboard = SwitchboardFactory.Create(hmax, vmax);

SwitchboardHelper.SetOccupied(switchboard, 1, 0);
SwitchboardHelper.SetInOut(switchboard, 1, 1, 0, 5);

SwitchboardHelper.Render(switchboard, hmax, vmax);