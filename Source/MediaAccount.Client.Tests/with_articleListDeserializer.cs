using System.Linq;
using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Resources;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Krowiorsch.MediaAccount
{
    class with_articleListDeserializer
    {
        Establish context = () =>
            _sut = new ArticleListDeserializer();

        protected static ArticleListDeserializer _sut;
    }

    [Subject("Deserialize")]
    class when_deserialze_a_sample_into_scroller : with_articleListDeserializer
    {
        Because of = () =>
        {
            _result = new ArticleListScroll(null);
            _sut.DeserializeInto(ResourceProvider.ProvideJsonByName("SampleArticleResponse"), _result);
        };
            
        It should_have_a_nextLink = () =>
            _result.NextPageLink.ShouldEqual("http://test.api.media-account2.de/api/v2/Articles?typ=Erscheinungsdatum&von=1466377200&bis=1466463600&page=2");

        It should_have_150_Articles = () =>
            _result.Items.Count().ShouldEqual(150);

        static ArticleListScroll _result;
    }
}