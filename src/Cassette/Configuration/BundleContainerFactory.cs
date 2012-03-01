﻿using System.Linq;

namespace Cassette.Configuration
{
    class BundleContainerFactory : BundleContainerFactoryBase
    {
        readonly BundleCollection bundles;

        public BundleContainerFactory(BundleCollection bundles, CassetteSettings settings)
            : base(settings)
        {
            this.bundles = bundles;
        }

        public override IBundleContainer CreateBundleContainer()
        {
            ProcessAllBundles(bundles);
            var externalBundles = CreateExternalBundlesUrlReferences(bundles);
            return new BundleContainer(bundles.Concat(externalBundles));
        }
    }
}