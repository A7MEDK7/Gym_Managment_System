using Shared.DTOs.TrainerDTOs;

namespace Services.Abstraction.Contract {
	public interface ITrainerService {
		Task<IEnumerable<TrainerDTO>> GetAllTrainers();
		Task<bool> CreateTrainer(CreateTrainerDTO createdTrainer);
        Task<TrainerDTO?> GetTrainerDetails(int trainerId);
		Task<TrainerToUpdateDTO?> GetTrainerToUpdate(int trainerId);
		Task<bool> UpdateTrainerDetails(TrainerToUpdateDTO updatedTrainer, int trainerId);
		Task<bool> RemoveTrainer(int trainerId);
	}
}
