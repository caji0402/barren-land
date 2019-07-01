# barren-land

The premise of this program is that you have a farm of 400m by 600m where coordinates of the field are from (0, 0) to (399, 599). A portion of the farm is barren, and all the barren land is in the form of rectangles. Due to these rectangles of barren land, the remaining area of fertile land is in no particular shape. An area of fertile land is defined as the largest area of land that is not covered by any of the rectangles of barren land. 

## Getting Started

### Prerequisites

Microsoft Visual Studio 2019 (If debugging or running unit tests)

.NET Framework 4.7.2

Windows 10 (may work with older versions of Windows but this has not been tested or verified)

### Installing & Running

Download this repository to your local machine in the directory of your choice.  This directory will be referred to as %INSTALL%.  It contains both the source code and the binaries to build and run the application.

From Visual Studio:

```
Open %INSTALL%/barren-land.sln
Select Build Solution from the Build menu
Select Start Debugging from the Debug menu
A command prompt will open and prompt you for input
```

From the command line:

```
Call %INSTALL%/barren-land/barren-land/bin/Release/barren-land.exe
A command prompt will open and prompt you for input
```

The user input for this program must be in a format which consists of four integers separated by single spaces, with no additional spaces in the string. The first two integers are the coordinates of the bottom left corner in the given rectangle, and the last two integers are the coordinates of the top right corner. If more than one rectangle is specified the subsequent rectangles should be separated by a command and space e.g. 

{"48 192 351 207", "48 392 351 407", "120 52 135 547", "260 52 275 547"}

{"0 292 399 307"}

Call "barren-land.exe -?" for available command line parameters

## Running the unit tests

From Visual Studio:

```
Open the Solution Explorer window if it is not already open (View -> Solution Explorer)
Right-Click the barren-land-test project in the Solution Explorer window and select Run Tests
Open the Test Explorer window (Test -> Windows -> Test Explorer)
Verify that all tests are passing
```
