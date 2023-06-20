from PIL import Image
import json

class Vec3:
  x = 0
  y = 0
  z = 0
  def __init__(self, x, y, z):
    self.x = x
    self.y = y
    self.z = z
  def toJson(self):
    return {
      "x": self.x,
      "y": self.y,
      "z": self.z
    }

class Vec2:
  x = 0
  y = 0
  def __init__(self, x, y):
    self.x = x
    self.y = y
  def toJson(self):
    return {
      "x": self.x,
      "y": self.y,
    }


class LevelData:
  PropData = []
  PlayerStartIndex = Vec2(0, 0)
  Size = Vec2(0, 0)
  def toJson(self):
    return {
      "PropData": self.PropData,
      "PlayerStartIndex": self.PlayerStartIndex.toJson(),
      "Size": self.Size.toJson()
    }


class BasicMoverInput:
  Enabled = True
  Offset = Vec3(0, 0, 0)
  Speed = 0.0
  StartLerp = 0.0
  def __init__(self, enabled, offset, speed, startLerp):
    self.Enabled = enabled
    self.Offset = offset
    self.Speed = speed
    self.StartLerp = startLerp
  def __init__(self):
    self.Enabled = False
    self.Offset = Vec3(0, 0, 0)
    self.Speed = 0.0
    self.StartLerp = 0.0
  def toJson(self):
    return {
      "Enabled": self.Enabled,
      "Offset": self.Offset.toJson(),
      "Speed": self.Speed,
      "StartLerp": self.StartLerp
    }


class PropData:
  Id = 0
  GridIndex = Vec2(0, 0)
  Direction = 0
  MoverInput = BasicMoverInput()
  def __init__(self, id, gridIndex, direction):
    self.Id = id
    self.GridIndex = gridIndex
    self.Direction = direction
  def toJson(self):
    return {
      "Id": self.Id,
      "GridIndex": self.GridIndex.toJson(),
      "Direction": self.Direction,
      "MoverInput": self.MoverInput.toJson()
    }

def AlphaToRot(alpha):
  if (alpha == 200):
    return 90
  if (alpha == 150):
    return 180
  if (alpha == 100):
    return 270
  return 0

def GetMoverInput(colour):
    moverInput = BasicMoverInput()
    if (colour == (0, 0, 0, 0)):
        return moverInput
    moverInput.Enabled = True
    moverInput.Offset = Vec3(colour[0] / 10.0, colour[1] / 10.0, 0)
    moverInput.Speed = colour[2] / 10.0
    if (colour[3] < 100):
        moverInput.Offset = Vec3(-moverInput.Offset.x, -moverInput.Offset.y, -moverInput.Offset.z)
    return moverInput

levelIndex = input("Stage Index: ")
mainReadPath = r"C:\Users\Michael\Desktop\Platformer_3D\Stages\Aseprite\Stage_" + levelIndex + r".png"
mainImage = Image.open(mainReadPath) # Can be many different formats.
mainPix = mainImage.load()

movementReadPath = r"C:\Users\Michael\Desktop\Platformer_3D\Stages\Aseprite\Stage_" + levelIndex + r"_m.png"
movementImage = Image.open(movementReadPath) # Can be many different formats.
movementPix = movementImage.load()



writePath = r"C:\Users\Michael\Unity Projects\Platformer_3D\Assets\Resources\Stages\Stage_" + levelIndex + r".json"


levelData = LevelData()
levelData.Size = Vec2(mainImage.width, mainImage.height)
levelData.PropData = []


for y in range(mainImage.height):
  for x in range(mainImage.width):
    mainColour = mainPix[x, y]
    mainColourRGB = (mainColour[0], mainColour[1], mainColour[2])
    mainAlpha = mainColour[3]
    gridIndex = Vec2(x, y)
    newProp = PropData(0, gridIndex, 0)

    if (mainColour == (0, 0, 0, 255)): 				#Ground
      newProp = PropData(1, gridIndex, 0)
    elif (mainColourRGB == (255, 0, 0)):			#Sawblade
      newProp = PropData(5, gridIndex, 0)
    elif (mainColourRGB == (255, 255, 0)):			#Spike Double
      newProp = PropData(4, gridIndex, 0)
    elif (mainColourRGB == (255, 100, 0)):			#Sawblade Block
      newProp = PropData(6, gridIndex, 0)
    elif (mainColourRGB == (0, 255, 0)):			#Sawblade_02
      newProp = PropData(6, gridIndex, 0)
      levelData.PropData.append(newProp.toJson())
      newProp = PropData(7, gridIndex, 0)
    elif (mainColourRGB == (255, 255, 255)):		#Completion Item
      newProp = PropData(3, gridIndex, 0)
    elif (mainColourRGB == (0, 0, 255)):			#Player
      levelData.PlayerStartIndex = Vec2(x, y)
      continue
    else:    							#Air
      continue
    newProp.MoverInput = GetMoverInput(movementPix[x, y])

    newProp.Direction = AlphaToRot(mainAlpha)
    levelData.PropData.append(newProp.toJson())


with open(writePath, "w") as outfile:
  json.dump(levelData.toJson(), outfile)





#im.show()

