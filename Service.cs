#region LICENSE

/*
Startup Creator - A simple application to effortlessly create Windows startup scripts.
Copyright (C) 2014 Soenneker LLC

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion LICENSE

#region

using System;

#endregion

namespace StartupCreator
{
    public class Service : IComparable
    {
        public string ActionName;
        public string DisplayName;
        public string Status;

        public Service(string displayName, string actionName, string status)
        {
            DisplayName = displayName;
            ActionName = actionName;
            Status = status;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Service otherService = obj as Service;
            if (otherService != null)
                return DisplayName.CompareTo(otherService.DisplayName);

            throw new ArgumentException("Object is not a Service");
        }
    }
}