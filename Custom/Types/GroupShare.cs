﻿using System;

namespace Custom.Types
{
    /// <summary>
    /// Provides information of a group ownCloud share.
    /// </summary>
	public class GroupShare : Share
    {
        /// <summary>
        /// Name of the user the target is being shared with
        /// </summary>
        public string SharedWith { get; set; }
    }
}

