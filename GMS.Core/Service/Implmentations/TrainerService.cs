using AutoMapper;
using Domin.Contract;
using Domin.Entities;
using Domin.GymEntities;
using Services.Abstraction.Contract;
using Shared.DTOs.TrainerDTOs;

namespace Services.Implmentations {
	public class TrainerService(IUnitOfWork unitOfWork, IMapper mapper) : ITrainerService {
		public async Task<bool> CreateTrainer(CreateTrainerDTO createdTrainer) {
			try {
                var Repo = unitOfWork.GetRepository<Trainer>();

				if (await IsEmailExist(createdTrainer.Email) || await IsPhoneExist(createdTrainer.Phone)) return false;

				var trainer = mapper.Map<Trainer>(createdTrainer);

				await Repo.AddAsync(trainer);

				return await unitOfWork.SaveChangesAsync() > 0;
			}
			catch (Exception) {
				return false;
			}
		}
		public async Task<IEnumerable<TrainerDTO>> GetAllTrainers() {
			var trainers = await unitOfWork.GetRepository<Trainer>().GetAllAsync();
			if (trainers is null || !trainers.Any()) return [];
			return mapper.Map<IEnumerable<TrainerDTO>>(trainers);
		}
		public async Task<TrainerDTO?> GetTrainerDetails(int trainerId) {
			var trainers = await unitOfWork.GetRepository<Trainer>().GetAsync(trainerId);
			if (trainers is null) return null;
            return mapper.Map<TrainerDTO>(trainers);
        }
		public async Task<TrainerToUpdateDTO?> GetTrainerToUpdate(int trainerId) {
			var trainer = await unitOfWork.GetRepository<Trainer>().GetAsync(trainerId);
			if (trainer is null) return null;
			return mapper.Map<TrainerToUpdateDTO>(trainer);
		}
		public async Task<bool> UpdateTrainerDetails(TrainerToUpdateDTO updatedTrainer, int trainerId) {
			var Repo = unitOfWork.GetRepository<Trainer>();
			var TrainerToUpdate = await Repo.GetAsync(trainerId);

			if (TrainerToUpdate is null || await IsEmailExist(trainerId, updatedTrainer.Email) || await IsPhoneExist(trainerId, updatedTrainer.Phone)) return false;

			TrainerToUpdate.Email = updatedTrainer.Email;
			TrainerToUpdate.Phone = updatedTrainer.Phone;
			TrainerToUpdate.Address.BuildingNumber = updatedTrainer.BuildingNumber;
			TrainerToUpdate.Address.Street = updatedTrainer.Street;
			TrainerToUpdate.Address.City = updatedTrainer.City;
			TrainerToUpdate.Specialties = updatedTrainer.Specialties;
			TrainerToUpdate.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            Repo.Update(TrainerToUpdate);

			return await unitOfWork.SaveChangesAsync() > 0;
		}
        public async Task<bool> RemoveTrainer(int trainerId) {
            var Repo = unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = await Repo.GetAsync(trainerId);
            if (TrainerToRemove is null || await HasActiveSessions(trainerId)) return false;
            Repo.Delete(TrainerToRemove);
            return await unitOfWork.SaveChangesAsync() > 0;
        }

        #region Helper Methods
        private async Task<bool> IsEmailExist(int trainerId, string email) {
            var trainerRepo = unitOfWork.GetRepository<Trainer>();
            // Check If User Email Already Exist
            var trainerEmail = await trainerRepo.GetAllAsync(m => m.Email == email && m.Id != trainerId);
            return trainerEmail.Any();
        }
        private async Task<bool> IsEmailExist(string email) {
            var trainerRepo = unitOfWork.GetRepository<Trainer>();
            // Check If User Email Already Exist
            var trainerEmail = await trainerRepo.GetAllAsync(m => m.Email == email);
            return trainerEmail.Any();
        }
        private async Task<bool> IsPhoneExist(int trainerId, string phone) {
            var trainerRepo = unitOfWork.GetRepository<Trainer>();
            // Check If User Email Already Exist
            var trainerPhoto = await trainerRepo.GetAllAsync(m => m.Phone == phone && m.Id != trainerId);
            return trainerPhoto.Any();
        }
        private async Task<bool> IsPhoneExist(string phone) {
            var trainerRepo = unitOfWork.GetRepository<Trainer>();
            // Check If User Email Already Exist
            var trainerPhoto = await trainerRepo.GetAllAsync(m => m.Phone == phone);
            return trainerPhoto.Any();
        }

        private async Task<bool> HasActiveSessions(int Id){
			var activeSessions = await unitOfWork.GetRepository<Session>().GetAllAsync(s => s.TrainerId == Id && s.StartDate > DateTime.Now);
			return activeSessions.Any();
        }
		#endregion
	}
}
