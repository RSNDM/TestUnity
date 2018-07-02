using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public interface IResLoadListener
    {
        void Finish(object asset);
        void Failure();
    }

