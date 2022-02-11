using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class TagServiceTest
    {
        private readonly TagService _service;
        private readonly Mock<ITagRepository> _repositoryMock = new Mock<ITagRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public TagServiceTest()
        {
            _service = new TagService(_unitOfWorkMock.Object, _repositoryMock.Object);
        }
        
        [Fact]
        public void GetAllAsyncTest()
        {
            var list = new List<Tag>();
            list.Add(new Tag(new TagID(new Guid()),new Nome("Java")));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            List<TagDTO> resultDTO = list.ConvertAll<TagDTO>(tag => TagDTOParser.ParaDTO(tag));
            var result =  _service.GetAllAsync();
            Assert.Equal(resultDTO.ToString(),result.Result.ToString());
        }
    }
}