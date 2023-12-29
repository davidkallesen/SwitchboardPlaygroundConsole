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
// For ease of testing, we just assume that the start and end cells are "straight" cells, i.e. the exit and entry points
// are on opposite sides of the cell.

// The program will print the grid to the console by calling the SwitchboardHelper.Render(switchboard, hmax, vmax);
// The switchboard is a 2D array of Cell objects. The hmax and vmax are the horizontal and vertical size of the grid.
using SwitchboardPlaygroundConsole;

var hmax = 6;
var vmax = 5;

// These are the start and end cells. I assume that the two cells are already existing in the grid,
// and have respectively their Out and In pointing "inside" the grid". 
// We want to find the lowest weighted connection from the Out of the start cell to the In of the end cell.
var startcell = new Point(4, 0 );
var targetcell = new Point(0, 3 );


var switchboard = new Switchboard(hmax, vmax);

switchboard.SetOccupied(new Point(1, 0));
switchboard.SetInOut(new Point( 1, 1), 0, 5);
switchboard.SetInOut(new Point(0, 3), 5, 3);
switchboard.SetInOut(new Point(1, 3), 7, 1);
switchboard.SetInOut(new Point(1, 2), 5, 1);
switchboard.SetInOut(new Point(0, 0), 4, 1);
switchboard.SetInOut(new Point(3, 3), 4, 4);
switchboard.SetInOut(new Point(4, 3), 4, 5);

ConsoleHelper.Render(switchboard);