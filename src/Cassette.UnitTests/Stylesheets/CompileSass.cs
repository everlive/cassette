﻿using Cassette.BundleProcessing;
using Cassette.Configuration;
using Moq;
using Xunit;

namespace Cassette.Stylesheets
{
    public class CompileSass_Tests
    {
        readonly CompileSass processor;
        readonly StylesheetBundle bundle;
        readonly Mock<IAsset> asset;

        public CompileSass_Tests()
        {
            processor = new CompileSass(Mock.Of<ICompiler>());
            bundle = new StylesheetBundle("~");
            asset = new Mock<IAsset>();
        }

        [Fact]
        public void GivenACompiler_WhenProcessCalled_ThenCompileAssetTransformerAddedToScssAsset()
        {
            asset.SetupGet(a => a.SourceFile.FullPath).Returns("test.scss");
            bundle.Assets.Add(asset.Object);

            processor.Process(bundle, new CassetteSettings(""));

            asset.Verify(a => a.AddAssetTransformer(It.Is<IAssetTransformer>(at => at is CompileAsset)));
        }

        [Fact]
        public void GivenACompiler_WhenProcessCalled_ThenCompileAssetTransformerAddedToSassAsset()
        {
            asset.SetupGet(a => a.SourceFile.FullPath).Returns("test.sass");
            bundle.Assets.Add(asset.Object);

            processor.Process(bundle, new CassetteSettings(""));

            asset.Verify(a => a.AddAssetTransformer(It.Is<IAssetTransformer>(at => at is CompileAsset)));
        }

        [Fact]
        public void GivenACompiler_WhenProcessCalled_ThenCompileAssetTransformerNotAddedToCssAsset()
        {
            asset.SetupGet(a => a.SourceFile.FullPath).Returns("test.css");
            bundle.Assets.Add(asset.Object);

            processor.Process(bundle, new CassetteSettings(""));

            asset.Verify(a => a.AddAssetTransformer(It.Is<IAssetTransformer>(at => at is CompileAsset)), Times.Never());
        }
    }
}