/********************************************************************

The Multiverse Platform is made available under the MIT License.

Copyright (c) 2012 The Multiverse Foundation

Permission is hereby granted, free of charge, to any person 
obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software 
is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
OR OTHER DEALINGS IN THE SOFTWARE.

*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Multiverse.Tools.WorldEditor
{
    public class AddObjectCommandFactory : ICommandFactory
    {
        WorldEditor app;
        IWorldContainer parent;

        public AddObjectCommandFactory(WorldEditor worldEditor, IWorldContainer parentObject)
        {
            app = worldEditor;
            parent = parentObject;
        }

        #region ICommandFactory Members

        public ICommand CreateCommand()
        {
            ICommand ret = null;
            using (AddObjectDialog dlg = new AddObjectDialog(app.Assets))
			{
				bool showAgain = false;
				DialogResult result;
				do
				{
					result = dlg.ShowDialog();
					showAgain = false;
					if (result == DialogResult.OK)
					{
						// do validation here
						// if validation fails, set showAgain to true
						showAgain = ((result == DialogResult.OK) && (!dlg.okButton_validating()));
					}

				} while (showAgain);
				if (result == DialogResult.OK)
				{
                    ret = new AddObjectCommand(app, parent, dlg.ObjectName, dlg.ObjectMeshName, dlg.RandomRotation, dlg.RandomScale, dlg.MinScale, dlg.MaxScale, dlg.MultiPlacement);
                }
            }

            return ret;
        }

        #endregion
    }
}
