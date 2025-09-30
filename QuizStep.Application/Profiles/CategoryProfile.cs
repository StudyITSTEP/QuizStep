using AutoMapper;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Application.DTOs;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<Category, UpdateCategoryCommand>();
    }
}