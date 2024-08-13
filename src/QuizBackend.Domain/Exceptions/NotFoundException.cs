using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceType { get; }
        public string ResourceId { get; }

        public NotFoundException(string resourceType, string resourceId)
            : base($"{resourceType} with id: {resourceId} doesn't exist")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }
}
