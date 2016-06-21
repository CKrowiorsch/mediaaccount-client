using Krowiorsch.MediaAccount.Model;
using Krowiorsch.MediaAccount.Resources;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Krowiorsch.MediaAccount
{
    public class with_articleListDeserializer
    {
        Establish context = () =>
            _sut = new ArticleListDeserializer();

        protected static ArticleListDeserializer _sut;
    }

    [Subject("Deserialize")]
    public class when_deserialze_a_sample : with_articleListDeserializer
    {
        Because of = () =>
            _result = _sut.Deserialize(ResourceProvider.ProvideJsonByName("SampleArticleResponse"));

        It should_have_a_nextLink = () =>
            _result.NextPageLink.ShouldEqual("http://test.api.media-account2.de/api/v2/Articles?typ=Erscheinungsdatum&von=1466377200&bis=1466463600&page=2");

        static ArticleListResponse _result;
    }
}